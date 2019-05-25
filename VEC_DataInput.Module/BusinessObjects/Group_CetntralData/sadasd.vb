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

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<DefaultClassOptions()>
<XafDisplayName("ข้อมูลนักศึกษาหลักสูตรระยะสั้นปี'60")>
Public Class Student_ShortCourse ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
    End Sub

    Private _CitizenID As String
    <XafDisplayName("บัตรประชาชน")>
    <Size(13)>
    Public Property CitizenID As String
        Get
            Return _CitizenID
        End Get
        Set(value As String)
            SetPropertyValue("CitizenID", _CitizenID, value)
        End Set
    End Property

    Private _TitleName As String
    <XafDisplayName("คำนำหน้าชื่อ")>
    Public Property TitleName As String
        Get
            Return _TitleName
        End Get
        Set(value As String)
            SetPropertyValue("TitleName", _TitleName, value)
        End Set
    End Property
    Private _FirstName As String
    <XafDisplayName("ชื่อ")>
    <Size(100)>
    Public Property FirstName As String
        Get
            Return _FirstName
        End Get
        Set(value As String)
            SetPropertyValue("FirstName", _FirstName, value)
        End Set
    End Property
    Private _LastName As String
    <XafDisplayName("นามสกุล")>
    <Size(100)>
    Public Property LastName As String
        Get
            Return _LastName
        End Get
        Set(value As String)
            SetPropertyValue("LastName", _LastName, value)
        End Set
    End Property
    Private _Gender As String
    <XafDisplayName("นามสกุล")>
    Public Property Gender As String
        Get
            Return _Gender
        End Get
        Set(value As String)
            SetPropertyValue("Gender", _Gender, value)
        End Set
    End Property
    Private _SubjectID As String
    <XafDisplayName("รหัสสาขาวิชา")>
    Public Property SubjectID As String
        Get
            Return _SubjectID
        End Get
        Set(value As String)
            SetPropertyValue("SubjectID", _SubjectID, value)
        End Set
    End Property
    Private _SubjectName As String
    <XafDisplayName("ชื่อวิชา")>
    Public Property SubjectName As String
        Get
            Return _SubjectName
        End Get
        Set(value As String)
            SetPropertyValue("SubjectName", _SubjectName, value)
        End Set
    End Property
    Private _MajorName As String
    <XafDisplayName("ชื่อสาขา")>
    Public Property MajorName As String
        Get
            Return _MajorName
        End Get
        Set(value As String)
            SetPropertyValue("MajorName", _MajorName, value)
        End Set
    End Property
    Private _CourseName As String
    <XafDisplayName("หลักสูตร")>
    Public Property CourseName As String
        Get
            Return _CourseName
        End Get
        Set(value As String)
            SetPropertyValue("CourseName", _CourseName, value)
        End Set
    End Property
    Private _ProjectName As String
    <XafDisplayName("โครงการ")>
    Public Property ProjectName As String
        Get
            Return _ProjectName
        End Get
        Set(value As String)
            SetPropertyValue("ProjectName", _ProjectName, value)
        End Set
    End Property
    Private _StudentStatus As String
    <XafDisplayName("สถานะนึกศึกษา")>
    Public Property StudentStatus As String
        Get
            Return _StudentStatus
        End Get
        Set(value As String)
            SetPropertyValue("StudentStatus", _StudentStatus, value)
        End Set
    End Property
    Private _Birthday As DateTime
    <XafDisplayName("วันเกิด")>
    Public Property Birthday As DateTime
        Get
            Return _Birthday
        End Get
        Set(value As DateTime)
            SetPropertyValue("Birthday", _Birthday, value)
        End Set
    End Property

    Private _StartDate As DateTime
    <XafDisplayName("วันที่เริ่มหลักสูตร")>
    Public Property StartDate As DateTime
        Get
            Return _StartDate
        End Get
        Set(value As DateTime)
            SetPropertyValue("StartDate", _StartDate, value)
        End Set
    End Property

    Private _EndDate As DateTime
    <XafDisplayName("วันที่จบหลักสูตร")>
    Public Property EndDate As DateTime
        Get
            Return _EndDate
        End Get
        Set(value As DateTime)
            SetPropertyValue("EndDate", _EndDate, value)
        End Set
    End Property

    Private _CourseTime As DateTime
    <XafDisplayName("จำนวนชั่วโมง")>
    Public Property CourseTime As DateTime
        Get
            Return _CourseTime
        End Get
        Set(value As DateTime)
            SetPropertyValue("CourseName", _CourseTime, value)
        End Set
    End Property

    Private _GeoName As String
    <XafDisplayName("ภาค")>
    Public Property GeoName As String
        Get
            Return _GeoName
        End Get
        Set(value As String)
            SetPropertyValue("GeoName", _GeoName, value)
        End Set
    End Property



End Class
