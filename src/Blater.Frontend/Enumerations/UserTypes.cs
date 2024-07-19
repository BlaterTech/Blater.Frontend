using System.Text.Json.Serialization;
using Blater.Attributes;
using NetEscapades.EnumGenerators;

namespace Blater.Frontend.Enumerations;

[Flags]
[EnumExtensions]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserTypes
{
    /// <summary>
    /// Anonymous user
    /// </summary>
    [ShortName("None")]
    None,
    /// <summary>
    ///     Client of the tenant AKA Final user
    /// </summary>
    [ShortName("B2C")]
    B2C,
    
    /// <summary>
    ///     The tenant user AKA Our client
    /// </summary>
    [ShortName("B2B")]
    B2B,
    
    /// <summary>
    ///     Has access to all portals features
    /// </summary>
    [ShortName("Gerente")]
    Manager,
    
    /// <summary>
    ///     Bypasses all security checks
    /// </summary>
    [ShortName("Admin")]
    Admin
}