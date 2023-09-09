namespace Money_Management
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
    static class Style
    {
        public static void SetStyleForm(Form form)
        {
            form.BackColor = Color.White;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Text = "Money Management";
            form.ClientSize = new Size(600, 500);
        }
    }
}