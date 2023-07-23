from tkinter import *

def AppWindow(users):
    """create and defined window settings"""
    app.title("Money Management")
    largeur_ecran = app.winfo_screenwidth()
    hauteur_ecran = app.winfo_screenheight()
    largeur_fenetre = (largeur_ecran // 10) * 8
    hauteur_fenetre = (hauteur_ecran // 10) * 8
    x_position = ((largeur_ecran - largeur_fenetre) // 2)
    y_position = ((hauteur_ecran - hauteur_fenetre) // 2) - ((hauteur_ecran - hauteur_fenetre) // 4)
    app.geometry(f"{largeur_fenetre}x{hauteur_fenetre}+{x_position}+{y_position}")
    app.configure(bg="#a4adf9")
    background_plate = Frame(app, bg="lightgrey", width=((largeur_fenetre/10)*4), height=((hauteur_fenetre/10)*8))
    background_plate.place(relx=0.5, rely=0.5, anchor=CENTER) 
    ContainSelectProfil(users, app)


def ContainSelectProfil(users, background_plate):
    """print on screen one button by users detect"""
    for i in range(1, len(users)):
        button = Button(background_plate, text="user : " + users[i], command=on_button_click)
        button.pack()

def usersInformations():
    """Return all users informations ..."""
    return ["Jossua", "Hella"]



#Button fonction
def on_button_click():
    return True


#Main
app = Tk()
users = usersInformations()
AppWindow(users)

app.mainloop()

