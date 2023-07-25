from tkinter import *


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
    background_plate = Canvas(app, bg="lightgrey", width=((windowWidth/10)*4), height=((windowHeight/10)*8))
    background_plate.place(relx=0.5, rely=0.5, anchor=CENTER)
    ContainSelectProfil(users, background_plate, windowWidth, windowHeight)


def ContainSelectProfil(users, background_plate, windowWidth, windowHeight):
    """print on screen one button by users detect"""
    ##locationY = 60;
    ##for i in range(len(users)):
     ##button = Button(background_plate, text="Utilisateur : " + users[i], command=on_button_click, bg="blue", fg="white", font=("Arial", 12), padx=10, pady=5)
     ##button.pack(pady=locationY)
     ##locationY = 20



     ######Test----------------------------------------------------------------------------

    #canvas = Canvas(app, bg="lightgrey", width=((windowWidth/10)*4), height=((windowHeight/10)*8), highlightthickness=0)
    #canvas.pack(fill="both")

    # Créer un Frame pour contenir les boutons
    #button_frame = Frame(canvas, bg="lightgrey")
    #canvas.create_window((((windowWidth/10)*4), ((windowHeight/10)*8)), window=button_frame, anchor="nw")

    for user in users:
        button = Button(background_plate, text=user,
                           bg="blue",       # Couleur de fond du bouton (ici bleu)
                           fg="white",      # Couleur du texte du bouton (ici blanc)
                           font=("Arial", 12),  # Police et taille du texte
                           padx=10,         # Espace horizontal (marge intérieure) du bouton
                           pady=5           # Espace vertical (marge intérieure) du bouton
                           )
        button.pack(fill="x", pady=2)

    # Définir une barre de défilement
    scrollbar = Scrollbar(app, command=background_plate.yview)
    background_plate.configure(yscrollcommand=scrollbar.set)
    scrollbar.pack(side="right", fill="y")

    # Configurer le Canvas pour gérer le défilement
    background_plate.bind_all("<MouseWheel>", lambda event: background_plate.yview_scroll(-1 * int(event.delta / 120), "units"))

    # Configurer la taille du Canvas pour qu'il s'ajuste en fonction du contenu
    background_plate.bind("<Configure>", lambda event: background_plate.configure(scrollregion=background_plate.bbox("all")))





    #FIN TEST ---------------------------------------------------------------------------------------------------------------------------------------


def usersInformations():
    """Return all users informations ..."""
    return ["Utilisateur 1", "Utilisateur 2", "Utilisateur 3", "Utilisateur 4", "Utilisateur 5, Utilisateur 1", "Utilisateur 2", "Utilisateur 3", "Utilisateur 4", "Utilisateur 5", "Utilisateur 1", "Utilisateur 2", "Utilisateur 3", "Utilisateur 4", "Utilisateur 111", "Utilisateur 222","Utilisateur 333","Utilisateur 444","Utilisateur 555"]



#Button fonction
def on_button_click():
    return True


#Main
app = Tk()
users = usersInformations()
AppWindow(users)

app.mainloop()

