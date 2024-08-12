using System.Diagnostics.CodeAnalysis;
using Blater.Models.Bases;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.Interfaces.AutoForm;

[SuppressMessage("Naming", "CA1716:Identificadores não devem corresponder a palavras-chave")]
public interface IAutoPropertyConfigurationBuilder<T, out TProperty> where T : BaseDataModel
{
}