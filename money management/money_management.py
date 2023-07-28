# -*- coding: utf-8 -*-
import sys
from PyQt5.QtWidgets import QApplication, QWidget
from tkinter import *

def AppWindow():
    """This function create a principal window of app and define style"""
    app = QApplication.instance() 
    if not app:
        app = QApplication(sys.argv)
    win = QWidget()
    win.setWindowTitle("Money Management")
    win.resize(SetWindowSettings("windowWidth"), SetWindowSettings("windowHeight"))
    win.move(SetWindowSettings("x_position"), SetWindowSettings("y_position"))
    win.setStyleSheet("background-color: #a4adf9;")
    win.show()
    app.exec_()

def SetWindowSettings(setting):
    """This function return size and position information for app"""
    root = Tk()
    screenWidth = root.winfo_screenwidth()
    screenHeight = root.winfo_screenheight()
    
    if (setting == "windowWidth"):
        return (screenWidth // 10) * 8
    elif (setting == "windowHeight"):
        return (screenHeight // 10) * 8
    elif (setting == "x_position"):
        widthWindow = (screenWidth // 10) * 8
        return ((screenWidth - widthWindow) // 2)
    elif (setting == "y_position"):
        windowHeight = (screenHeight // 10) * 8
        return ((screenHeight - windowHeight) // 2) - ((screenHeight - windowHeight) // 4)

def UserList():
    """Return User List"""
    return ("User1", "User2", "User3","User4","User5","User6","User7","User8","User9","User10",)


AppWindow()

#https://courspython.com/interfaces.html