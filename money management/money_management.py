# -*- coding: utf-8 -*-
import sys
from PyQt5.QtWidgets import QApplication, QWidget
from tkinter import *
from PyQt5.QtWidgets import *

def AppWindow():
    """This function create a principal window of app and define style"""
    app = QApplication.instance() 
    if not app:
        app = QApplication(sys.argv)
    win = QMainWindow()
    win.setWindowTitle("Money Management")
    win.resize(SetWindowSettings("windowWidth"), SetWindowSettings("windowHeight"))
    win.move(SetWindowSettings("x_position"), SetWindowSettings("y_position"))
    win.setStyleSheet("background-color: #a4adf9;")
    UserButtonList(win)
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

def UserButtonList(window):
    button_texts = ["Bouton 1", "Bouton 2", "Bouton 3", "Bouton 4", "Bouton 5", "Bouton 6", "Bouton 7", "Bouton 8", "Bouton 9", "Bouton 10", "Bouton 11", "Bouton 12"]
    button_list = create_button_list(window, button_texts)
    button_list.setMinimumWidth(200)
    button_list.setMaximumWidth(200)
    scroll_area = QScrollArea()
    scroll_area.setWidgetResizable(True)
    scroll_area.setWidget(button_list)
    #test
    h_layout = QHBoxLayout()
    h_layout.addStretch(1)  # Ajouter un espace extensible pour centrer sur l'axe x
    h_layout.addWidget(scroll_area)
    h_layout.addStretch(1)  # Ajouter un espace extensible pour centrer sur l'axe x

    # Créer un QVBoxLayout pour centrer le QHBoxLayout sur l'axe y
    v_layout = QVBoxLayout()
    v_layout.addStretch(1)  # Ajouter un espace extensible pour centrer sur l'axe y
    v_layout.addLayout(h_layout)
    v_layout.addStretch(1)  # Ajouter un espace extensible pour centrer sur l'axe y

    central_widget = QWidget()
    central_widget.setLayout(v_layout)
    window.setCentralWidget(central_widget)

def create_button_list(parent, button_texts):
    layout = QVBoxLayout()
    for text in button_texts:
        button = QPushButton(text)
        layout.addWidget(button)
    widget = QWidget(parent)
    widget.setLayout(layout)
    return widget

def UserList():
    """Return User List"""
    return ("User1", "User2", "User3","User4","User5","User6","User7","User8","User9","User10",)


AppWindow()

#https://courspython.com/interfaces.html