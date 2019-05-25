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
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports System.Globalization

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<XafDisplayName("ผลงานสิ่งประดิษฐ์(สำนักวิจัย)"), NavigationItem("Group_Research")>
<DefaultClassOptions()> _
Public Class Invention ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Try
            Dim myUser As User
            myUser = CType(SecuritySystem.CurrentUser, User)
            If myUser.InstitutionID IsNot Nothing Then
                Dim objnstitution As Institution = Session.FindObject(Of Institution)(CriteriaOperator.Parse("Oid=?", CType(SecuritySystem.CurrentUser, User).InstitutionID.Oid))
                Institution = objnstitution
                provinceID = objnstitution.ref_mas_province
                centerId = objnstitution.ref_mas_province.ref_mas_vecCenter
                geoId = objnstitution.ref_mas_province.ref_mas_vecCenter.ref_mas_geo
            ElseIf myUser.vecCenterID IsNot Nothing Then

                Dim objcenterId As mas_vecCenter = Session.FindObject(Of mas_vecCenter)(CriteriaOperator.Parse("Oid=?", CType(SecuritySystem.CurrentUser, User).vecCenterID.Oid))
                centerId = objcenterId
                geoId = objcenterId.ref_mas_geo
            End If


            Dim yearNow As String = DateTime.Now.ToString("yyyy", New CultureInfo("th-TH"))
            Dim edd As mas_Year = Session.FindObject(Of mas_Year)(CriteriaOperator.Parse("YearName=?", yearNow))
            If edd IsNot Nothing Then
                EducateYear = edd
            Else
                edd = New mas_Year(Session)
                edd.YearName = yearNow
                EducateYear = edd
            End If
        Catch ex As Exception

        End Try

        ' Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).

    End Sub

    <ImmediatePostData(), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)>
    Public ReadOnly Property userCenter() As Boolean
        Get
            Dim myUser As User
            myUser = CType(SecuritySystem.CurrentUser, User)
            If myUser.vecCenterID IsNot Nothing Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    <ImmediatePostData(), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)>
    Public ReadOnly Property userIntitution() As Boolean
        Get
            Dim myUser As User
            myUser = CType(SecuritySystem.CurrentUser, User)
            If myUser.InstitutionID IsNot Nothing Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Private _Year As String
    <XafDisplayName("ปี")>
    <Persistent("Year")>
    <Browsable(False)>
    <Size(4)>
    Public ReadOnly Property Year As String
        Get
            Return _Year
        End Get

    End Property


    Private _EducateYear As mas_Year
    <XafDisplayName("ปีการศึกษา"), Size(4), VisibleInDetailView(True), VisibleInListView(True), RuleRequiredField(DefaultContexts.Save), Index(0)>
    Public Property EducateYear As mas_Year
        Get
            Return _EducateYear
        End Get
        Set(value As mas_Year)
            SetPropertyValue("EducateYear", _EducateYear, value)
            If EducateYear IsNot Nothing Then
                _Year = EducateYear.YearName
            End If
        End Set
    End Property


    Private _geoId As mas_geo
    <ImmediatePostData()>
    <XafDisplayName("ศูนย์ฯ ภาค"), RuleRequiredField(DefaultContexts.Save), Index(1)>
    <Appearance("EnablegeoId_Center", Enabled:=False, Context:="DetailView", Criteria:="userCenter")>
    <Appearance("EnablegeoId_Intitution", Enabled:=False, Context:="DetailView", Criteria:="userIntitution")>
    Public Property geoId As mas_geo
        Get
            Return _geoId
        End Get
        Set(value As mas_geo)
            SetPropertyValue("geoId", _geoId, value)

        End Set
    End Property

    Private _RegionName As String
    <XafDisplayName("ภาค")>
    <Browsable(False)>
    Public ReadOnly Property RegionName As String
        Get
            Return _RegionName
        End Get

    End Property


    Private _centerId As mas_vecCenter
    <DataSourceProperty("geoId.vecCenters"), ImmediatePostData()>
    <Appearance("EnablecenterId_Center", Enabled:=False, Context:="DetailView", Criteria:="userCenter")>
    <Appearance("EnablecenterId_Intitution", Enabled:=False, Context:="DetailView", Criteria:="userIntitution")>
    <XafDisplayName("ศูนย์ฯ กลุ่มจังหวัด"), RuleRequiredField(DefaultContexts.Save), Size(2), Index(2)>
    Public Property centerId As mas_vecCenter
        Get
            Return _centerId
        End Get
        Set(value As mas_vecCenter)
            SetPropertyValue("centerId", _centerId, value)
        End Set
    End Property


    Private _provinceID As mas_province
    <DataSourceProperty("centerId.Provinces"), ImmediatePostData()>
    <Appearance("EnableprovinceID_Intitution", Enabled:=False, Context:="DetailView", Criteria:="userIntitution")>
    <XafDisplayName("จังหวัด"), RuleRequiredField(DefaultContexts.Save), Index(3)>
    Public Property provinceID As mas_province
        Get
            Return _provinceID
        End Get
        Set(value As mas_province)
            SetPropertyValue("provinceID", _provinceID, value)
        End Set
    End Property

    Private _Institution As Institution
    <XafDisplayName("สถานศึกษา")>
    <DataSourceProperty("provinceID.Institutions"), ImmediatePostData(), Index(4)>
    <Appearance("EnableInstitution_Intitution", Enabled:=False, Context:="DetailView", Criteria:="userIntitution")>
    <RuleRequiredField(DefaultContexts.Save)>
    Public Property Institution() As Institution
        Get
            Return _Institution
        End Get
        Set(ByVal value As Institution)
            SetPropertyValue("Institution", _Institution, value)
            If Institution IsNot Nothing Then
                _InstitutionName = Institution.InstitutionName
            End If
        End Set
    End Property


    Private _InstitutionName As String
    <XafDisplayName("สถานศึกษา")>
    <Persistent("InstitutionName")>
    <Browsable(False)>
    Public ReadOnly Property InstitutionName As String
        Get
            Return _InstitutionName
        End Get

    End Property

    Private _RecDate As DateTime
    <XafDisplayName("วันที่บันทึก")>
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)>
    <Index(5)>
    Public Property RecDate As DateTime
        Get
            Return _RecDate
        End Get
        Set(value As DateTime)
            SetPropertyValue("RecDate", _RecDate, value)
        End Set
    End Property


    Private _InventionType As String
    <XafDisplayName("ประเภท")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(2)>
    Public Property InventionType As String
        Get
            Return _InventionType
        End Get
        Set(value As String)
            SetPropertyValue("InventionType", _InventionType, value)
        End Set
    End Property


    Private _CompetitionLevel As String
    <XafDisplayName("ระดับ")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(6)>
    Public Property CompetitionLevel As String
        Get
            Return _CompetitionLevel
        End Get
        Set(value As String)
            SetPropertyValue("CompetitionLevel", _CompetitionLevel, value)
        End Set
    End Property


    Private _Recperson As String
    <XafDisplayName("ผู้บันทึก")>
    <RuleRequiredField(DefaultContexts.Save)>
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)>
    <Index(7)>
    Public Property Recperson As String
        Get
            Return _Recperson
        End Get
        Set(value As String)
            SetPropertyValue("Recperson", _Recperson, value)
        End Set
    End Property


    Private _Email As String
    <XafDisplayName("Email")>
    <RuleRequiredField(DefaultContexts.Save)>
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)>
    <Index(8)>
    Public Property Email As String
        Get
            Return _Email
        End Get
        Set(value As String)
            SetPropertyValue("Email", _Email, value)
        End Set
    End Property

    Private _Phone As String
    <XafDisplayName("โทรศัพท์")>
    <RuleRequiredField(DefaultContexts.Save)>
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)>
    <Index(9)>
    Public Property Phone As String
        Get
            Return _Phone
        End Get
        Set(value As String)
            SetPropertyValue("Phone", _Phone, value)
        End Set
    End Property



    Private _ProvinceName As String
    <XafDisplayName("จังหวัด")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(10)>
    Public Property ProvinceName As String
        Get
            Return _ProvinceName
        End Get
        Set(value As String)
            SetPropertyValue("ProvinceName", _ProvinceName, value)
        End Set
    End Property




    Private _Abstract As String
    <XafDisplayName("บทคัดย่อ")>
    <RuleRequiredField(DefaultContexts.Save)>
    Public Property Abstract As String
        Get
            Return _Abstract
        End Get
        Set(value As String)
            SetPropertyValue("Abstract", _Abstract, value)
        End Set
    End Property

    Private _Attribute As String
    <XafDisplayName("คุณลักษณะ")>
    <RuleRequiredField(DefaultContexts.Save)>
    Public Property Attribute As String
        Get
            Return _Attribute
        End Get
        Set(value As String)
            SetPropertyValue("Attribute", _Attribute, value)
        End Set
    End Property

    Private _ProjectCost As String
    <XafDisplayName("ค่าใช้จ่ายโครงงาน")>
    <RuleRequiredField(DefaultContexts.Save)>
    Public Property ProjectCost As String
        Get
            Return _ProjectCost
        End Get
        Set(value As String)
            SetPropertyValue("ProjectCost", _ProjectCost, value)
        End Set
    End Property

    Private _TeacherTotal As String
    <XafDisplayName("จำนวนครู")>
    <RuleRequiredField(DefaultContexts.Save)>
    Public Property TeacherTotal As String
        Get
            Return _TeacherTotal
        End Get
        Set(value As String)
            SetPropertyValue("TeacherTotal", _TeacherTotal, value)
        End Set
    End Property

    Private _Shot As String
    <XafDisplayName("ระยะสั้น")>
    <RuleRequiredField(DefaultContexts.Save)>
    Public Property Shot As String
        Get
            Return _Shot
        End Get
        Set(value As String)
            SetPropertyValue("Shot", _Shot, value)
        End Set
    End Property



    Private _PVCH As Integer
    <XafDisplayName("ปวช.")>
    <RuleRequiredField(DefaultContexts.Save)>
    Public Property PVCH As Integer
        Get
            Return _PVCH
        End Get
        Set(value As Integer)
            SetPropertyValue("PVCH", _PVCH, value)
        End Set
    End Property


    Private _PVS As Integer
    <XafDisplayName("ปวส.")>
    <RuleRequiredField(DefaultContexts.Save)>
    Public Property PVS As Integer
        Get
            Return _PVS
        End Get
        Set(value As Integer)
            SetPropertyValue("PVS", _PVS, value)
        End Set
    End Property

    Private _PTS As Integer
    <XafDisplayName("ปทส.")>
    <RuleRequiredField(DefaultContexts.Save)>
    Public Property PTS As Integer
        Get
            Return _PTS
        End Get
        Set(value As Integer)
            SetPropertyValue("PTS", _PTS, value)
        End Set
    End Property

    Private _Picture1 As Integer
    <XafDisplayName("ภาพ1")>
    <RuleRequiredField(DefaultContexts.Save)>
    Public Property Picture1 As Integer
        Get
            Return _Picture1
        End Get
        Set(value As Integer)
            SetPropertyValue("Picture1", _Picture1, value)
        End Set
    End Property

    Private _Picture2 As Integer
    <XafDisplayName("ภาพ2")>
    <RuleRequiredField(DefaultContexts.Save)>
    Public Property Picture2 As Integer
        Get
            Return _Picture2
        End Get
        Set(value As Integer)
            SetPropertyValue("Picture2", _Picture2, value)
        End Set
    End Property





    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
