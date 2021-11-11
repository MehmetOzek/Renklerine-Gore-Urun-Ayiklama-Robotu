
Imports System.IO
Imports System
Imports System.ComponentModel
Imports System.Threading
Imports System.IO.Ports
Public Class Form1

    Dim veri1 = 0
    Dim veri2 = 0
    Dim veri3 = 0               ' RENK DEĞERLERİ İÇİN DEĞİŞKENLER TANIMLANDI ;       '
    Dim veri4 = 0
    Dim veri5 = 0
    Dim veri6 = 0
    Dim portlar As Array
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        portlar = IO.Ports.SerialPort.GetPortNames()

        For i = 0 To UBound(portlar)
            ComboBox1.Items.Add(portlar(i))
        Next
        ComboBox1.Text = ComboBox1.Items.Item(0)    'COMBOBOX'A PORT SAYISI YAZDIRILDI '

        Button2.Enabled = False
        Button3.Enabled = False
        Button4.Enabled = False

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click   ' BAĞLAN BUTONU '
        TextBox1.Text = 0
        TextBox2.Text = 0
        TextBox3.Text = 0
        TextBox4.Text = 0
        TextBox5.Text = 0
        TextBox6.Text = 0
        TextBox7.Text = 0


        SerialPort1.PortName = ComboBox1.Text
        SerialPort1.BaudRate = 9600                                  'SECİLEN PORTA BAGLANTI YAPILIYOR'
        SerialPort1.DataBits = 8
        SerialPort1.Parity = IO.Ports.Parity.None
        SerialPort1.StopBits = IO.Ports.StopBits.One


        SerialPort1.Open()

        Button1.Enabled = False
        Button2.Enabled = True
        Button4.Enabled = True
        Button3.Enabled = False



    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click     ' BAĞLANTI KES BUTONU '


        SerialPort1.Close()

        Button1.Enabled = True
        Button2.Enabled = False
        Button4.Enabled = False
        Button3.Enabled = False

    End Sub
    Private Sub cmbPort_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If SerialPort1.IsOpen = False Then
            SerialPort1.PortName = ComboBox1.Text      ' BAĞLANTI AÇIKKEN DEĞİŞİM OLMAZ '
        Else
            MsgBox("BAĞLANTIYI, KAPALIYKEN DEĞİŞTİRİNİZ", vbCritical)
        End If
    End Sub

    Dim carpanx As Integer = 1
    Dim carpany As Integer = 1
    Dim sonportsayisi As Integer = 0
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        portlar = IO.Ports.SerialPort.GetPortNames()
        If sonportsayisi <> portlar.Length Then
            ComboBox1.Items.Clear()
            For i = 0 To UBound(portlar)
                ComboBox1.Items.Add(portlar(i))
            Next
            ComboBox1.Text = ComboBox1.Items.Item(0)

            sonportsayisi = portlar.Length
        End If





    End Sub



    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click  'CALISTIR BUTONU '
        SerialPort1.Write("1")
        Button4.Enabled = False
        Button3.Enabled = True
        Button1.Enabled = False
        Button2.Enabled = False
        'Timer1.Start()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click  'DURDUR BUTONU '
        SerialPort1.Write("0")
        Button1.Enabled = False
        Button2.Enabled = True
        Button4.Enabled = True
        Button3.Enabled = False
        'Timer1.Stop()
    End Sub
    Dim kontrol As Integer = 0  'SAYI DEĞİSKENİ TANIMLANTI
    Private Sub SerialPort1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        Try
            Dim renkDegeri As Integer = SerialPort1.ReadLine()  'porta yazdırdığımız renk okuma case 'i okunuyor değeri dönüyor '
            Me.Invoke(Sub()
                          If renkDegeri = 1 Then
                              veri1 = veri1 + 1
                              TextBox1.Text = veri1
                              TextBox8.Text = "pembe"
                              kontrol = 0
                          ElseIf renkDegeri = 2 Then
                              veri2 = veri2 + 1
                              TextBox2.Text = veri2
                              TextBox8.Text = "sarı"
                              kontrol = 0
                          ElseIf renkDegeri = 3 Then
                              veri3 = veri3 + 1
                              TextBox3.Text = veri3
                              TextBox8.Text = "yesil"
                              kontrol = 0
                          ElseIf renkDegeri = 4 Then
                              veri4 = veri4 + 1
                              TextBox4.Text = veri4
                              TextBox8.Text = "mavi"
                              kontrol = 0
                          ElseIf renkDegeri = 5 Then
                              veri5 = veri5 + 1
                              TextBox5.Text = veri5
                              TextBox8.Text = "kırmızı"
                              kontrol = 0
                          ElseIf renkDegeri = 6 Then
                              veri6 = veri6 + 1
                              TextBox6.Text = veri6
                              TextBox8.Text = "turuncu"
                              kontrol = 0
                          ElseIf renkDegeri = 0 Then
                              kontrol += 1
                              TextBox8.Text = "Ürün Yok"
                          End If

                          If kontrol > 0 Then
                              SerialPort1.Write("0")
                              Button1.Enabled = False
                              Button2.Enabled = True
                              Button4.Enabled = True
                              Button3.Enabled = False
                              MsgBox(" ürün okuma tamamlandı ! ")

                          End If
                          TextBox7.Text = veri1 + veri2 + veri3 + veri4 + veri5 + veri6

                      End Sub)
        Catch ex As Exception

        End Try




    End Sub


End Class

