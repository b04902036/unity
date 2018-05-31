from django.shortcuts import render
from django.http import HttpResponse, HttpResponseRedirect
from django import template
from .models import Question, Choice, Predict
from .forms import FileForm
import cv2 as cv
import numpy as np
import os
from django.views.decorators.csrf import csrf_exempt
from django.template import RequestContext
from . import face_recognition
def hello_world(request, index):
	return HttpResponse("hello!!" + str(index))
@csrf_exempt 
def index(request):
	t = template.loader.get_template('app1/index.html')
	choice_list = Choice.objects.all()
	return HttpResponse(t.render(locals(), request))
@csrf_exempt 
def upload(request):
	if(request.POST):
		# create a form instance and populate it with data from the request:
		form = FileForm(request.POST, request.FILES)
		# check whether it's valid:
		print (request.POST['name'])
		if form.is_valid():
			# process the data in form.cleaned_data as required
			# ...
			# redirect to a new URL:
			f = open('save_uploaded_file', 'wb')
			count = 0
			print ('count')
			for chunk in request.FILES['file'].chunks():
				count += 1
				print ('count', count)
				f.write(chunk)
			f.close()
			img = cv.imread('save_uploaded_file')
			gray = cv.cvtColor(img, cv.COLOR_BGR2GRAY)
			array = []
			addarray = []
			for i in range(256):
				array.append(0)
				addarray.append(0)
			gray = np.array(gray)
			for i in range(gray.shape[0]):
				for j in range(gray.shape[1]):
					array[gray[i][j]] += 1
			all_num = gray.shape[0] * gray.shape[1]
			count = 0
			for i in range(256):
				count = count + array[i]
				addarray[i] = 255.0 * count / all_num
			for i in range(gray.shape[0]):
				for j in range(gray.shape[1]):
					gray[i][j] = addarray[gray[i][j]]
			gray = gray.astype(np.uint8)
			cv.imwrite('save_uploaded_file_gray.jpg', gray)
			try:
				image2 = face_recognition.load_image_file('save_uploaded_file_gray.jpg')
				image = face_recognition.load_image_file('save_uploaded_file')
				face_landmarks_list = face_recognition.face_landmarks(image)
				face_landmarks_list2 = face_recognition.face_landmarks(image2)
				print (face_landmarks_list, face_landmarks_list2)
				print ('whyyyyyyyyyyyy')
				if(face_landmarks_list != [] and face_landmarks_list2 != []):
					result = True
				else:
					result = False
			except:
				result = False
			if(result):
				c = Predict.objects.all()
				if(c):
					c = Predict.objects.get(id=1)
					c.result = True
				else:
					c = Predict(result=True)
				c.save()
			else:
				c = Predict.objects.all()
				if(c):
					c = Predict.objects.get(id=1)
					c.result = False
				else:
					c = Predict(result=False)
				c.save()
			return HttpResponseRedirect('/app1/predict')
	# if a GET (or any other method) we'll create a blank form
	else:
		form = FileForm()
	t = template.loader.get_template('app1/upload.html')
	return HttpResponse(t.render(locals(), request))
@csrf_exempt 
def predict(request):
	c = Predict.objects.all()
	t = template.loader.get_template('app1/predict.html')
	if(not c):
		sign = 0
		return HttpResponse(t.render(locals(), request))
	else:
		sign = 1
		c = Predict.objects.get(id=1)
		if(c.result):
			result = 1
		else:
			result = 0
		return HttpResponse(t.render(locals(), request))