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
    ##locationY = 60;
    ##for i in range(len(users)):
     ##button = Button(background_plate, text="Utilisateur : " + users[i], command=on_button_click, bg="blue", fg="white", font=("Arial", 12), padx=10, pady=5)
     ##button.pack(pady=locationY)
     ##locationY = 20



     ######Test----------------------------------------------------------------------------


    canvas = Canvas(app, bg="lightgrey", width=400, height=300, highlightthickness=0)
    canvas.pack(fill="both", expand=True)

    # Créer un Frame pour contenir les boutons
    button_frame = Frame(canvas, bg="lightgrey")
    canvas.create_window((50, 50), window=button_frame, anchor="nw")

    for user in users:
        button = Button(button_frame, text=user,
                           command=lambda u=user: on_button_click(u),
                           bg="blue",       # Couleur de fond du bouton (ici bleu)
                           fg="white",      # Couleur du texte du bouton (ici blanc)
                           font=("Arial", 12),  # Police et taille du texte
                           padx=10,         # Espace horizontal (marge intérieure) du bouton
                           pady=5           # Espace vertical (marge intérieure) du bouton
                           )
        button.pack(fill="x", pady=2)

    # Définir une barre de défilement
    scrollbar = Scrollbar(app, command=canvas.yview)
    canvas.configure(yscrollcommand=scrollbar.set)
    scrollbar.pack(side="right", fill="y")

    # Configurer le Canvas pour gérer le défilement
    #canvas.bind_all("<MouseWheel>", lambda event: canvas.yview_scroll(-1 * int(event.delta / 120), "units"))

    # Configurer la taille du Canvas pour qu'il s'ajuste en fonction du contenu
    #canvas.bind("<Configure>", lambda event: canvas.configure(scrollregion=canvas.bbox("all")))

    app.update_idletasks()
    canvas_frame = canvas.create_window((canvas.winfo_width() // 2, canvas.winfo_height() // 2), window=button_frame, anchor="center")
    canvas.configure(scrollregion=canvas.bbox("all"))


def usersInformations():
    """Return all users informations ..."""
    return ["Utilisateur 1", "Utilisateur 2", "Utilisateur 3", "Utilisateur 4", "Utilisateur 5"]



#Button fonction
def on_button_click():
    return True


#Main
app = Tk()
users = usersInformations()
AppWindow(users)

app.mainloop()

