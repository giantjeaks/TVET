Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Linq
Imports System.Data.Entity
Imports System.Data.Common
Imports System.Data.Entity.Core.Objects
Imports System.Data.Entity.Infrastructure
Imports System.ComponentModel
Imports DevExpress.ExpressApp.EF.Updating
Imports DevExpress.Persistent.BaseImpl.EF
Imports DevExpress.Persistent.BaseImpl.EF.PermissionPolicy

' Business Model Design with Entity Framework - https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113462.aspx
' Use the Entity Framework Code First in XAF - https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113466.aspx
Public Class VEC_DataInputDbContext
	Inherits DbContext
	Public Sub New(connectionString As [String])
		MyBase.New(connectionString)
	End Sub
	Public Sub New(connection As DbConnection)
		MyBase.New(connection, False)
	End Sub
	Public Sub New()
		MyBase.New("name=ConnectionString")
	End Sub
    Private m_ModulesInfo As DbSet(Of ModuleInfo)
    Public Property ModulesInfo() As DbSet(Of ModuleInfo)
        Get
            Return m_ModulesInfo
        End Get
        Set(value As DbSet(Of ModuleInfo))
            m_ModulesInfo = value
        End Set
    End Property
	Private m_ModelDifferences As DbSet(Of ModelDifference)
    Public Property ModelDifferences() As DbSet(Of ModelDifference)
        Get
            Return m_ModelDifferences
        End Get
        Set(value As DbSet(Of ModelDifference))
            m_ModelDifferences = value
        End Set
    End Property
    Private m_ModelDifferenceAspects As DbSet(Of ModelDifferenceAspect)
    Public Property ModelDifferenceAspects() As DbSet(Of ModelDifferenceAspect)
        Get
            Return m_ModelDifferenceAspects
        End Get
        Set(value As DbSet(Of ModelDifferenceAspect))
            m_ModelDifferenceAspects = value
        End Set
    End Property
End Class