import os

def SayHello():
	return "Hello! I'm talking form PYTHON script !"

def Test(message):
	directory = os.getcwd()
	return message + " | " + directory


