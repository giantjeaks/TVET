Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.ExpressApp.DC
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.Base
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Model
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.Security
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Net
Imports System.IO
Imports System.Xml


<DefaultClassOptions()>
Public Class CustomLogin ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    Inherits AuthenticationBase
    Implements IAuthenticationStandard
    Private customLogonParameters As CustomLogonParameters ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    Public Sub New()
        customLogonParameters = New CustomLogonParameters()
    End Sub

    Public Overrides Sub Logoff()
        MyBase.Logoff()
        customLogonParameters = New CustomLogonParameters()
    End Sub

    Function CheckRole_UserAll(By_NameRole As String, By_UserName As User) As Boolean
        Dim Temp_ As String = False

        If By_UserName IsNot Nothing Then
            For Each irole As Role In By_UserName.Roles
                If irole.Name = "SMECompany" Then
                    Return True
                End If
            Next
        End If

        Return Temp_
    End Function


    Public Overrides Sub ClearSecuredLogonParameters()
        customLogonParameters.Password = ""
        MyBase.ClearSecuredLogonParameters()
    End Sub

#Region "Function API"
    ''' <summary>
    ''' ฟังก์ชั่นสำหรับ login ผ่าน api บนเว็บระบบ DITP
    ''' </summary>
    ''' <param name="username">Username</param>
    ''' <param name="password">Password</param>
    ''' <returns></returns>
    Function CheckLogin(ByVal username As String, ByVal password As String) As String
        'ตรวจสอบข้อมูล user ผ่าน webservice
        Dim result_post As String
        Dim jsonSring As String = ""
        Dim data = Encoding.UTF8.GetBytes(jsonSring)
        Return result_post
    End Function
    ''' <summary>
    ''' Call Website module
    ''' </summary>
    ''' <param name="uri"></param>
    ''' <param name="jsonDataBytes"></param>
    ''' <param name="contentType"></param>
    ''' <param name="method"></param>
    ''' <returns></returns>
    Private Function SendRequest(uri As Uri, jsonDataBytes As Byte(), contentType As String, method As String) As String

        Dim req As WebRequest = WebRequest.Create(uri)
        'System.Net.ServicePointManager.UseNagleAlgorithm = False
        'System.Net.ServicePointManager.Expect100Continue = False
        req.ContentType = contentType
        req.Method = method
        req.ContentLength = jsonDataBytes.Length


        Dim stream = req.GetRequestStream()
        stream.Write(jsonDataBytes, 0, jsonDataBytes.Length)
        stream.Close()

        Dim response = req.GetResponse().GetResponseStream()

        Dim reader As New StreamReader(response)
        Dim res = reader.ReadToEnd()
        reader.Close()
        response.Close()

        Return res
    End Function
#End Region

    Public Overrides Function Authenticate(ByVal objectSpace As IObjectSpace) As Object
        Dim employeeTemp As User
        Try
            'Dim employeeTemp As User = objectSpace.FindObject(Of User)(New BinaryOperator("UserName", "administrator"))
            Dim Arr As Array
            If customLogonParameters.UserName.Contains("#Token") Then


                Arr = customLogonParameters.UserName.Split("#")
                customLogonParameters.UserName = Arr(0)
                employeeTemp = objectSpace.FindObject(Of User)(New BinaryOperator("UserName", customLogonParameters.UserName))
                If employeeTemp IsNot Nothing Then
                    Return employeeTemp
                End If

            Else
                employeeTemp = objectSpace.FindObject(Of User)(New BinaryOperator("UserName", customLogonParameters.UserName))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return employeeTemp
    End Function

    Public Overrides Sub SetLogonParameters(ByVal logonParameters As Object)
        Me.customLogonParameters = DirectCast(logonParameters, CustomLogonParameters)
    End Sub

    Public Overrides Function GetBusinessClasses() As IList(Of Type)
        Return New Type() {GetType(CustomLogonParameters)}
    End Function
    Public Overrides ReadOnly Property AskLogonParametersViaUI() As Boolean
        Get
            Return True
        End Get
    End Property
    Public Overrides ReadOnly Property LogonParameters() As Object
        Get
            Return customLogonParameters
        End Get
    End Property
    Public Overrides ReadOnly Property IsLogoffEnabled() As Boolean
        Get
            Return True
        End Get
    End Property
End Class
