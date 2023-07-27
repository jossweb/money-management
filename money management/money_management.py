# -*- coding: utf-8 -*-
import sys
from PyQt5.QtWidgets import *
import tkinter as tk


def AppWindow():

    
    app = QApplication.instance() 
    if not app:
        app = QApplication(sys.argv)

    fen = QWidget()
    fen.setWindowTitle("Money Management")
    fen.setStyleSheet("gradient_style = background: lineargradient(x1: 0, y1: 0, x2: 1, y2: 1, stop: 0 #a4adf9, stop: 1 #ff6b6b);")

    # on fixe la taille de la fenêtre
    fen.resize(SetWindowSettings("windowWidth"), SetWindowSettings("windowHeight"))

    # on fixe la position de la fenêtre
    fen.move(SetWindowSettings("x_position"), SetWindowSettings("y_position"))
    fen.show()
    app.exec_()


def SetWindowSettings(setting):
    root = tk.Tk()
    
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
    
AppWindow()
#https://courspython.com/interfaces.html