using Blater.Frontend.Client.Auto.AutoBuilders;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Models.Bases;
using FluentValidation;

namespace Blater.Frontend.Client.Models.Bases;

public abstract class BaseFrontendModel<TModel> : BaseDataModel, IAutoConfiguration<TModel>
{
    public abstract void Configure(AutoModelConfigurationBuilder<TModel> builder);
}