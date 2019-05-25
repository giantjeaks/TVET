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
<DefaultClassOptions()>
<XafDisplayName("ทะเบียนครูพิเศษอาชีวศึกษาอุตสาหกรรม 4.0 & 10 อุตสาหกรรมใน EEC")>
<RuleCriteria("TeacherNotZero", DefaultContexts.Save, "NumOfTeacherReg > 0", "จำนวนครูที่ขึ้นทะเบียนอย่างน้อย 1 คน")>
Public Class EEC_Indus4TeacherReg ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
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
                InstitutionID = objnstitution
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


    End Sub
    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        Me.LastUpdated = Date.Now
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


    'Private _EducationYear As String
    '<XafDisplayName("ปีการศึกษา"), VisibleInListView(True), VisibleInDetailView(True), Size(4), RuleRequiredField(DefaultContexts.Save)>
    'Public Property EducationYear() As String
    '    Get
    '        Return _EducationYear
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue("EducationYear", _EducationYear, value)
    '    End Set
    'End Property


    'ใช้จำกัดปีที่อยากให้แสดงใน EducateYear
    <Browsable(False)>
    Public ReadOnly Property SpecificYear As XPCollection(Of mas_Year)
        Get
            Dim qry = From c In Session.Query(Of mas_Year)
                      Where CType(c.YearName, Integer) >= 2561
                      Select c.Oid

            Return New XPCollection(Of mas_Year)(Session, New InOperator("Oid", qry))
        End Get
    End Property


    Private _EducateYear As mas_Year
    <DataSourceProperty("SpecificYear")>
    <XafDisplayName("ปีการศึกษา"), Size(4), VisibleInDetailView(True), VisibleInListView(True), RuleRequiredField(DefaultContexts.Save), Index(0)>
    Public Property EducateYear As mas_Year
        Get
            Return _EducateYear
        End Get
        Set(value As mas_Year)
            SetPropertyValue("EducateYear", _EducateYear, value)
        End Set
    End Property

    Private _geoId As mas_geo
    <ImmediatePostData()>
    <XafDisplayName("ศูนย์ฯ ภาค"), RuleRequiredField(DefaultContexts.Save)>
    <Appearance("EnablegeoId_Center", Enabled:=False, Context:="DetailView", Criteria:="userCenter")>
    <Appearance("EnablegeoId_Intitution", Enabled:=False, Context:="DetailView", Criteria:="userIntitution")>
    Public Property geoId As mas_geo
        Get
            Return _geoId
        End Get
        Set(ByVal value As mas_geo)
            SetPropertyValue("geoId", _geoId, value)
        End Set
    End Property


    Private _centerId As mas_vecCenter
    <DataSourceProperty("geoId.vecCenters"), ImmediatePostData()>
    <Appearance("EnablecenterId_Center", Enabled:=False, Context:="DetailView", Criteria:="userCenter")>
    <Appearance("EnablecenterId_Intitution", Enabled:=False, Context:="DetailView", Criteria:="userIntitution")>
    <XafDisplayName("ศูนย์ฯ กลุ่มจังหวัด"), RuleRequiredField(DefaultContexts.Save)>
    Public Property centerId As mas_vecCenter
        Get
            Return _centerId
        End Get
        Set(ByVal value As mas_vecCenter)
            SetPropertyValue("centerId", _centerId, value)
        End Set
    End Property

    Private _provinceID As mas_province
    <DataSourceProperty("centerId.Provinces"), ImmediatePostData()>
    <Appearance("EnableprovinceID_Intitution", Enabled:=False, Context:="DetailView", Criteria:="userIntitution")>
    <XafDisplayName("จังหวัด"), RuleRequiredField(DefaultContexts.Save)>
    Public Property provinceID As mas_province
        Get
            Return _provinceID
        End Get
        Set(ByVal value As mas_province)
            SetPropertyValue("provinceID", _provinceID, value)
        End Set
    End Property

    Private _InstitutionID As Institution
    <Appearance("EnableInstitutionID_Intitution", Enabled:=False, Context:="DetailView", Criteria:="userIntitution")>
    <DataSourceProperty("provinceID.Institutions"), ImmediatePostData()>
    <XafDisplayName("สถานศึกษา"), RuleRequiredField(DefaultContexts.Save)>
    Public Property InstitutionID As Institution
        Get
            Return _InstitutionID
        End Get
        Set(ByVal value As Institution)
            SetPropertyValue("InstitutionID", _InstitutionID, value)
        End Set
    End Property

    Private _ExpertiseType As mas_Expertise
    <XafDisplayName("ประเภทความเชี่ยวชาญ"), RuleRequiredField(DefaultContexts.Save)>
    <ModelDefault("LookupProperty", "ExpertiseType")>
    Public Property ExpertiseType As mas_Expertise
        Get
            Return _ExpertiseType
        End Get
        Set(ByVal value As mas_Expertise)
            SetPropertyValue("ExpertiseType", _ExpertiseType, value)
        End Set
    End Property

    Private _NumOfTeacherReg As Integer
    <XafDisplayName("จำนวนครูที่ขึ้นทะเบียน"), RuleRequiredField("Num_of_Teacher", DefaultContexts.Save), VisibleInDetailView(True), VisibleInListView(True), RuleRequiredField(DefaultContexts.Save)>
    Public Property NumOfTeacherReg As Integer
        Get
            Return _NumOfTeacherReg
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("NumOfTeacherReg", _NumOfTeacherReg, value)
        End Set
    End Property

    Private _LastUpdated As DateTime
    <XafDisplayName("วันที่ Update ล่าสุด"), VisibleInListView(False), VisibleInDetailView(False)>
    Public Property LastUpdated As DateTime
        Get
            Return _LastUpdated
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("LastUpdated", _LastUpdated, value)
        End Set
    End Property

End Class
