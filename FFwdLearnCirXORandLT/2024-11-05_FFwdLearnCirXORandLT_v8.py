import random
import numpy as np
import torch
import torch.nn.functional as F
import math
import matplotlib
import matplotlib.pyplot as plt
from timeit import default_timer as timer

def build_dataset_CIRCLE(n_datapoints, g, device):
    """
    Create a dataset of 2D input coordinates and output values
    which are an index into the catgory the coordinate e.g. outside and inside
    Xinp : tensor of 2D input vectors
    Yout : tensor of scalar outputs that match Xi such that
        it has a value of 0 if the X coordinate does NOT fall inside the pattern
        and 1 if the X coordinate DO fall inside the pattern
    """
    r = 0.3
    Xinp,Yout = [],[]

    # create a uniform distribution of input vectors over the range of 0 to 1
    Xinp = torch.rand((n_datapoints, 2), generator=g, device=device)
    # shift ALL input vectors to be centered on 0,0
    Xinp -= 0.5

    # 1 = inside the circle (of radius = r)
    # 0 = outside the circle
    def categorize(xinp):
        dist = math.sqrt(xinp[0]**2 + xinp[1]**2)
        if dist <= r:
            v = 1
        else:
            v = 0
        return v

    # Map all input coordinates into 2 categories; inside or outside of the circle
    Yout = torch.tensor([categorize(xinp) for xinp in Xinp])

    return Xinp,Yout

def build_dataset_LESSTHAN(n_datapoints, g, device):
    """
    Create a dataset of 2D input coordinates and output values
    which are an index into the catgory the coordinate e.g. outside and inside
    Xinp: tensor of 2D input vectors
    Yout: tensor of scalar outputs that match Xi such that
          it has a value of 0 if the X coordinate does NOT fall inside the pattern
          and 1 if the X coordinate DO fall inside the pattern
    """
    Xinp,Yout = [],[]

    # create a uniform distribution of input vectors over the range of 0 to 1
    Xinp = torch.rand((n_datapoints, 2), generator=g, device=device)
    # shift ALL input vectors to be centered on 0,0
    Xinp -= 0.5

    # 1 = x is less than y
    # 0 = x is equal to or greater than y
    def categorize(xinp):
        if xinp[0] < xinp[1]:
            v = 1
        else:
            v = 0
        return v

    # Map all input coordinates into 2 categories; inside or outside of the circle
    Yout = torch.tensor([categorize(xinp) for xinp in Xinp])

    return Xinp,Yout

def build_dataset_XOR(n_datapoints, g, device):
    """
    Create a dataset of 2D input coordinates and output values
    which are an index into the catgory the coordinate e.g. outside and inside
    Xinp: tensor of 2D input vectors
    Yout: tensor of scalar outputs that match Xi such that
          it has a value of 0 if the X coordinate does NOT fall inside the pattern
          and 1 if the X coordinate DO fall inside the pattern
    """
    Xinp,Yout = [],[]

    # create a uniform distribution of input vectors over the range of 0 to 1
    Xinp = torch.rand((n_datapoints, 2), generator=g, device=device)
    # shift ALL input vectors to be centered on 0,0
    Xinp -= 0.5

    # 1 = x is less than y
    # 0 = x is equal to or greater than y
    def categorize(xinp):
        if (xinp[0] < 0) and (xinp[1] < 0):
            v = 0
        elif (xinp[0] < 0) and (xinp[1] > 0):
            v = 1
        elif (xinp[0] > 0) and (xinp[1] < 0):
            v = 1
        else:
            v = 0
        return v

    # Map all input coordinates into 2 categories; inside or outside of the circle
    Yout = torch.tensor([categorize(xinp) for xinp in Xinp])

    return Xinp,Yout
    
def convertToprob(v):
    """
    Convert the input value (from 0.0 to 1.0)
    to a 1x2 vector where V[0] is the probability that v is 0
    and V[1] is the probability that v is 1
    and we *know* that v is either exactly 0.0 or 1.0
    """
    V = [[0.0, 0.0],
         [0.0, 0.0]]
    if math.fabs(v-1.0) < 0.1:
        V[0] = 0.9999 #1.0  !FIX: do I need to do this?
        V[1] = 0.0001 #0.0
    elif math.fabs(v-0.0) < 0.1:
        V[0] = 0.0001 #0.0
        V[1] = 0.9999 #1.0
    else:
        V[0] = 0.5
        V[1] = 0.5
    return V

def plot_dataset(Xinp, Yout, title):
    # Scatter plot of all data
    x_coord = Xinp[:, 0]
    y_coord = Xinp[:, 1]
    def color(y):
        if math.fabs(y-1.0) < 0.1:
            return 'green'
        elif math.fabs(y-0.0) < 0.1:
            return 'red'
        else:
            return 'gray'
    colors = [color(y.item()) for y in Yout]
    
    plt.scatter(x_coord, y_coord, color=colors)
    plt.xlabel('X')
    plt.ylabel('Y')
    plt.title(f'Data Points ({title})')
    plt.xlim(-0.5, 0.5)
    plt.ylim(-0.5, 0.5)
    plt.show()
    
# ##############################################################################

def main():
    """
    Main function
    """

    # Use the CPU or GPU
    device = torch.device("cpu")
    #device = torch.device("cuda")

    g = torch.Generator(device=device).manual_seed(2147483647)

    n_points = 100000

    print('Choose training set:')
    print('0 - Circle')
    print('1 - Less-Than')
    print('2 - XOR')
    print('Enter 0,1,2> ', end='')
    choice = int(input())

    if (choice == 0):
        # Generate the data set for a CIRCLE
        Xinp_all, Yout_all = build_dataset_CIRCLE(n_points, g, device)
        datasetname = 'CIRCLE'
    elif (choice == 1):
        # Generate the data set for a LESSTHAN
        Xinp_all, Yout_all = build_dataset_LESSTHAN(n_points, g, device)
        datasetname = 'LESSTHAN'
    else:
        # Generate the data set for a XOR
        Xinp_all, Yout_all = build_dataset_XOR(n_points, g, device)
        datasetname = 'XOR'

    # split: training, dev/validation, test
    #80% : training
    #10% : hyper parameters e.g. # neurons (train "hyper parameters")
    #10% : testing
    n1 = int(0.8 * len(Xinp_all))
    n2 = int(0.9 * len(Xinp_all))
    Xtr, Ytr = Xinp_all[:n1], Yout_all[:n1]
    Ytr_p = torch.tensor([convertToprob(y) for y in Ytr], device=device)
    Xdev, Ydev = Xinp_all[n1:n2], Yout_all[n1:n2]
    Ydev_p = torch.tensor([convertToprob(y) for y in Ydev], device=device)
    Xte, Yte = Xinp_all[n2:], Yout_all[n2:]
    Yte_p = torch.tensor([convertToprob(y) for y in Yte], device=device)

    #!FIX: problem loss is 1.0 or higher BUT should be no worse than 0.5
    #zero bias and reduce weights?

    # Plot the training data
    #plot_dataset(Xtr, Ytr, datasetname + ' (training)')

    # ##########################################################################
    n_inp = 2       # 2 inputs x,y coordinate into function
    n_hidden = 9
    n_out = 2       # 2 outputs ==> prob of inside, prob of outside
    W1 = torch.randn((n_inp,n_hidden), generator=g, device=device) * 1.00
    b1 = torch.randn(n_hidden, generator=g, device=device) * 1.00
    W2 = torch.randn((n_hidden, n_out), generator=g, device=device) * 1.00
    b2 = torch.randn(n_out, generator=g, device=device) * 1.00
    parameters = [W1, b1, W2, b2]
    
    for p in parameters:
        p.requires_grad = True

    # init learning rate
    lr = 0.05

    lossi = []
    stepi = []

    n_epoch = 5
    batch_size = 10 # sample this many inputs and outputs
    n_batches = 10000
    
    for j in range(n_epoch):
        print(f'LR = {lr}')

        for i in range(n_batches):

            # minibatch construct
            ix = torch.randint(0, Xtr.shape[0], (batch_size,), device=device)
            Xb, Yb = Xtr[ix], Ytr_p[ix] # sample from inputs and outputs
            
            # forward pass
            hpreact = Xb @ W1 + b1
            h = torch.tanh(hpreact)
            logits = h @ W2 + b2
            loss = F.cross_entropy(logits, Yb)
            
            if (i % 1000) == 0:
                print(f'loss: {loss.item()}')

            # backward pass
            for p in parameters:
                p.grad = None
            loss.backward()

            # update parameters
            for p in parameters:
                p.data += -lr * p.grad  # const learning rate

            # track stats
            stepi.append(i)
            lossi.append(loss.log10().item())

        # learning rate decay in the final steps
        if (j >= 3):
            lr *= 0.1

    # plot the loss vs step
    """
    plt.plot(stepi, lossi)
    plt.show()
    """

    # #####################################################
    # TEST
    # forward pass
    hpreact = Xdev @ W1 + b1
    h = torch.tanh(hpreact)
    logits = h @ W2 + b2
    loss = F.cross_entropy(logits, Ydev_p)
    print('')
    print(f'Test on Dev loss: {loss}')

    # Sample from model
    g2 = torch.Generator(device=device).manual_seed(2147483647 + 10)

    # plot ALL data for Xdev and logits
    probs = F.softmax(logits, dim=1)
    ix = torch.multinomial(probs, num_samples=1, generator=g2)

    print(f'plot')
    print(f'Xdev: {Xdev.shape} {Xdev.dtype}')
    print(f'ix: {ix.shape} {ix.dtype}')
    #print(ix[:10])
    """
    Xdev: torch.Size([10000, 2])
    ix: torch.Size([10000, 1])
    """
    plot_dataset(Xdev,ix, 'TESTING Dev')
    # #####################################################
    
# ##############################################################################

if __name__ == '__main__':
    main()
