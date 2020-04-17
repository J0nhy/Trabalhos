Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        GerirLigacao.IniciarLigacao(My.Settings.BaseTrabalhosConnectionString)
        Dim login As New LoginForm1
        login.MdiParent = Me
        login.Show()
        GerirMenu.Enabled = False
        TrabalhosMenu.Enabled = False


    End Sub

    Private Sub GerirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TrabalhosMenu.Click

        Dim t As New Trabalhos
        t.MdiParent = Me
        t.Show()

    End Sub

    Private Sub GerirMenu_Click(sender As Object, e As EventArgs) Handles GerirMenu.Click

        Dim u As New Utilizadores
        u.MdiParent = Me
        u.Show()

    End Sub
End Class
