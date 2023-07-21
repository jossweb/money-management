import tkinter as tk


def AppWindow():
    app = tk.Tk()
    app.title("Money Management")
    largeur_ecran = app.winfo_screenwidth()
    hauteur_ecran = app.winfo_screenheight()
    largeur_fenetre = (largeur_ecran // 10) * 8
    hauteur_fenetre = (hauteur_ecran // 10) * 8
    x_position = ((largeur_ecran - largeur_fenetre) // 2)
    y_position = ((hauteur_ecran - hauteur_fenetre) // 2) - ((hauteur_ecran - hauteur_fenetre) // 4)
    app.geometry(f"{largeur_fenetre}x{hauteur_fenetre}+{x_position}+{y_position}")
    app.configure(bg="#a4adf9")
    app.mainloop()

AppWindow()

