import sys
import numpy as np
import cv2 as cv
result = open(sys.argv[2], 'w')
face_cascade = cv.CascadeClassifier('haarcascades/haarcascade_frontalface_default.xml')
eye_cascade = cv.CascadeClassifier('haarcascades/haarcascade_eye.xml')
img = cv.imread(sys.argv[1])
cv.imshow('img',img)
cv.waitKey(0)
cv.destroyAllWindows()
'''
try:	
	img = cv.imread(sys.argv[1])
	print (np.shape(img))
	gray = cv.cvtColor(img, cv.COLOR_BGR2GRAY)
	faces = face_cascade.detectMultiScale(gray, 1.1, 1, minSize=(30, 30))
	print ('faces', faces)
	print ('len of faces', len(faces))
	if(faces != ()):
		for (x,y,w,h) in faces:
			cv.rectangle(img,(x,y),(x+w,y+h),(255,0,0),2)
			roi_gray = gray[y:y+h, x:x+w]
			roi_color = img[y:y+h, x:x+w]
			eyes = eye_cascade.detectMultiScale(roi_gray, 1.5, 1)
			print ('eyes', eyes)
			print ('len of eyes', len(eyes))
			if(eyes != ()):
				result.write('1\n')
				#exit()
			else:
				continue
			for (ex,ey,ew,eh) in eyes:
				cv.rectangle(roi_color,(ex,ey),(ex+ew,ey+eh),(0,255,0),2)
		cv.imshow('img',img)
		cv.waitKey(0)
		cv.destroyAllWindows()
		result.write('0\n')
	else:
		result.write('0\n')
except SystemExit:
	pass
except:
	print ('why')
	result.write('0\n')
result.close()
'''