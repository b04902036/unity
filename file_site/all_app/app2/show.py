import numpy as np
import cv2 as cv
import sys
img = cv.imread(sys.argv[1])
img = np.array(img)
print (img.shape)
cv.imshow(winname='show', mat=img)
cv.waitKey(0)