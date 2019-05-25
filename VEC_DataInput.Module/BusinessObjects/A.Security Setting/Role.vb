' Developer Express Code Central Example:
' How to: Implement a Security System User Based on an Existing Business Class
' 
' The complete description is available in the How to: Implement a Security System
' User Based on an Existing Business Class
' (http://help.devexpress.com/#Xaf/CustomDocument3452) help topic.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E4160

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.ExpressApp.Security.Strategy
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.DC

<XafDisplayName("บทบาท/หน้าที่")>
<ImageName("BO_Role")> _
Public Class Role
    Inherits SecuritySystemRoleBase

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Private _Description As String
    <XafDisplayName("คำอธิบาย")>
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)>
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Description", _Description, value)
        End Set
    End Property

    <Association("Users-Roles")> _
    Public ReadOnly Property Users() As XPCollection(Of User)
        Get
            Return GetCollection(Of User)("Users")
        End Get
    End Property
End Class
