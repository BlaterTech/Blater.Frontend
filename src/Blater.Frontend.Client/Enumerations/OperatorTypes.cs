using System.ComponentModel;
using NetEscapades.EnumGenerators;

namespace Blater.Frontend.Client.Enumerations;

[EnumExtensions]
public enum OperatorTypes
{
    [Description("CONTAINS")]
    Contains,
    
    [Description("AND")]
    And,

    [Description("OR")]
    Or,

    [Description("NOT")]
    Not,

    [Description("IN")]
    In,

    [Description("LIKE")]
    Like,

    [Description("IS NULL")]
    IsNull,

    [Description("IS NOT NULL")]
    IsNotNull,

    [Description("EMPTY")]
    Empty,

    [Description("NOT EMPTY")]
    NotEmpty,

    [Description("BETWEEN")]
    Between
}