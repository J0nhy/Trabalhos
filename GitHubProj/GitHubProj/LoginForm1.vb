Imports System.Data.OleDb
Public Class LoginForm1

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See https://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Public objCon As New OleDbConnection
    Public strSQL As String

    Public strConnection As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\BaseTrabalhos.mdb"
    Public da As New OleDbDataAdapter
    Public ds As New DataSet
    Public tentativas As Integer = 0
    Public strNivel As Integer = 0



    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Dim aviso As String = "Aviso!" & vbNewLine & vbNewLine +
               "Se tentativas= 3 " + vbNewLine +
               "Aplicação será encerrada."

        If (UsernameTextBox.Text = "") And (PasswordTextBox.Text = "") Then
            MessageBox.Show("Forneça um Nome de utilizador e uma Senha." &
                vbNewLine + vbNewLine + aviso, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ElseIf UsernameTextBox.Text = "" Then
            MessageBox.Show("Forneça um Nome de utilizador." &
                vbNewLine + vbNewLine + aviso, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ElseIf PasswordTextBox.Text = "" Then
            MessageBox.Show("Forneça uma Senha." &
                vbNewLine + vbNewLine + aviso, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else

            Dim strName = UsernameTextBox.Text
            Dim strPass = PasswordTextBox.Text
            With objCon
                .Close()
                If .State = ConnectionState.Closed Then
                    .ConnectionString = strConnection
                    .Open()
                End If
            End With

            ds.Clear()
            strSQL = "SELECT * FROM Usuario WHERE Login='" &
            strName & "' AND Password ='" & strPass & "'"


            da = New OleDbDataAdapter(strSQL, objCon)
            da.Fill(ds, "Usuario")
            If ds.Tables("Usuario").Rows.Count <> 0 Then
                MessageBox.Show("Bem vindo(a) " & strName & "." & vbNewLine +
                                "Autenticado com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)

                strNivel = ds.Tables("Usuario").Rows(0).Item("Nivel")


                Form1.MenuStrip1.Enabled = True
                Me.Close()

            Else

                MessageBox.Show("O Nome de Utilizador e a Senha nao sao validos." & vbNewLine +
                                "Tente novamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

        If strNivel = 3 Then
            Form1.GerirMenu.Enabled = True
            Form1.TrabalhosMenu.Enabled = True
        Else
            Form1.TrabalhosMenu.Enabled = True
            Form1.GerirMenu.Enabled = False

        End If

        If (tentativas = 3) Then
            MessageBox.Show("Atingiu o numero maximo de tentativas." & vbNewLine &
                            "Aplicação vai ser encerrada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click

        If objCon.State = ConnectionState.Open Then
            objCon.Close()
        End If

        Dim msg As String = "Deseja sair da aplicadol"
        Dim titulo As String = "Terminar"
        Dim botoes As MessageBoxButtons = MessageBoxButtons.YesNo
        Dim icone As MessageBoxIcon = MessageBoxIcon.Exclamation
        Dim resultado As DialogResult = MessageBox.Show(msg, titulo, botoes, icone)
        If resultado = DialogResult.Yes Then
            Me.Close()
            Application.Exit()
        End If




    End Sub

    Private Sub LoginForm1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
