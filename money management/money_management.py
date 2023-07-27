# -*- coding: utf-8 -*-
from tkinter import *
from PIL import Image, ImageTk


def SetWindowSettings(screenWidth, screenHeight, setting):
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

def AppWindow(users):
    """create and defined window settings"""
    #Get screen size ...
    screenWidth = app.winfo_screenwidth()
    screenHeight = app.winfo_screenheight()

    #Set Window settings
    windowWidth = SetWindowSettings(screenWidth, screenHeight, "windowWidth")
    windowHeight = SetWindowSettings(screenWidth, screenHeight, "windowHeight")
    x_position = SetWindowSettings(screenWidth, screenHeight, "x_position")
    y_position = SetWindowSettings(screenWidth, screenHeight, "y_position")


    app.title("Money Management")
    app.geometry(f"{windowWidth}x{windowHeight}+{x_position}+{y_position}")
    app.configure(bg="#a4adf9")
    background_plate = Canvas(app, bg="lightgrey", width=((windowWidth/10)*2), height=((windowHeight/10)*8))
    background_plate.place(relx=0.5, rely=0.5, anchor=CENTER)
    ProfilMenu(background_plate, users)
    AddUserButton()

def AddUserButton():    
    #new_window_button = Button(app, text="+", bg="blue", fg="white", font=("Arial", 12), padx=10, pady=5)
    #new_window_button.pack(side=BOTTOM, padx=20, pady=20, anchor=SE)
    circle_image = Image.open("img/addButton.png")  # Remplacez "circle.png" par le chemin de votre image de cercle
    #circle_image = circle_image.resize((50, 50))  # Ajustez la taille du cercle selon vos besoins
    circle_image = ImageTk.PhotoImage(circle_image)

    # Créer le bouton rond avec l'image de cercle comme arrière-plan
    new_window_button = Button(app, image=circle_image, borderwidth=0)
    new_window_button.pack(side=BOTTOM, padx=20, pady=20, anchor=SE)

def ProfilMenu(app, users):
    scrollbar = Scrollbar(app)
    scrollbar.pack( side = RIGHT, fill = Y)
    liste = Listbox(app, yscrollcommand = scrollbar.set, font=("Arial", 15), justify="center", selectbackground="lightgrey", selectforeground="black", activestyle="none")
    for user in users:
       liste.insert(END, user)
       liste.itemconfigure(END, bg="white", )


    liste.pack(side = LEFT, fill = BOTH )
    scrollbar.config(command = liste.yview )

def usersInformations():
    """Return all users informations ..."""
    return ["FIGUEIRAS Jossua", "vingt caractere max1", "Utilisateur 3", "Utilisateur 4", "Utilisateur 5"]



#Button fonction
def on_button_click():
    return True


#Main
app = Tk()
users = usersInformations()
AppWindow(users)

app.mainloop()

