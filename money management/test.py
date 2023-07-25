import tkinter as tk

def on_button_click(button_text):
    label.config(text="Le bouton '{}' a été cliqué !".format(button_text))

def AppWindow(users):
    # ... Votre code précédent ...

    # Créer le widget Canvas pour le fond avec des coins arrondis
    canvas = tk.Canvas(app, bg="lightgrey", width=((windowWidth/10)*4), height=((windowHeight/10)*8), highlightthickness=0)
    canvas.place(relx=0.5, rely=0.5, anchor=tk.CENTER)

    # Créer un Frame pour contenir les boutons
    button_frame = tk.Frame(canvas, bg="lightgrey")
    canvas.create_window((0, 0), window=button_frame, anchor=tk.NW)

    # Créer les boutons dans le Frame
    for user in users:
        button = tk.Button(button_frame, text=user,
                           bg="blue",       # Couleur de fond du bouton (ici bleu)
                           fg="white",      # Couleur du texte du bouton (ici blanc)
                           font=("Arial", 12),  # Police et taille du texte
                           padx=10,         # Espace horizontal (marge intérieure) du bouton
                           pady=5           # Espace vertical (marge intérieure) du bouton
                           )
        button.pack(fill="x", pady=2)

    # Définir une barre de défilement
    scrollbar = tk.Scrollbar(app, command=canvas.yview)
    canvas.configure(yscrollcommand=scrollbar.set)
    scrollbar.pack(side="right", fill="y")

    # Configurer le Canvas pour gérer le défilement
    canvas.bind_all("<MouseWheel>", lambda event: canvas.yview_scroll(-1 * int(event.delta / 120), "units"))

    # Configurer la taille du Canvas pour qu'il s'ajuste en fonction du contenu
    canvas.bind("<Configure>", lambda event: canvas.itemconfig(canvas_frame, width=event.width))

    # Placer le Canvas au centre de la fenêtre principale (vertical et horizontal)
    canvas.update_idletasks()
    canvas_frame = canvas.create_window((canvas.winfo_width() // 2, canvas.winfo_height() // 2), window=button_frame, anchor=tk.CENTER)
    canvas.configure(scrollregion=canvas.bbox("all"))

app = tk.Tk()
users = ["Utilisateur 1", "Utilisateur 2", "Utilisateur 3", "Utilisateur 4", "Utilisateur 5", "Utilisateur 1", "Utilisateur 2", "Utilisateur 3", "Utilisateur 4", "Utilisateur 5", "Utilisateur 1", "Utilisateur 2", "Utilisateur 3", "Utilisateur 4", "Utilisateur 111", "Utilisateur 222","Utilisateur 333","Utilisateur 444","Utilisateur 555"]
AppWindow(users)

app.mainloop()