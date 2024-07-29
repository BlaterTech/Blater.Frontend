using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Blater.Frontend.Auto.AutoTable.Implementations;
using Blater.Frontend.Auto.AutoTable.Interfaces;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Auto.AutoTable.Configurations;

[SuppressMessage("Performance", "CA1822:Marcar membros como estáticos")]
public class Foo : BaseDataModel
{
    public int Quantity { get; set; }

    private class FooConfiguration : ITableConfiguration<Foo>
    {
        public void Configure(TableConfigurationBuilder<Foo> builder)
        {
            builder.ToTable("Foo's");

            builder
                .Property(x => x.Quantity)
                .Order(5)
                .Order(5)
                .Order(5)
                .HasColumnName("Qtd")
                .MaxLength(5)
                .MergeColumn(2)
                .Class("mud-width-full")
                .Style("color: red")
                .IsRequired()
                .HasValidation(value => value.Quantity > 0, "Quantity must be greater than 0")
                .HasValidation(v =>
                {
                    v.AddValidation(new RangeAttribute(1, 100) { ErrorMessage = "Quantity must be between 1 and 100" });
                    v.AddValidation(new RequiredAttribute { ErrorMessage = "Quantity is required" });
                })
                .ComponentType("ComponentName, creating enum component name")
                .OnValueChanged(EventCallback.Factory.Create<int>(this, value => OnQuantityChanged(builder, value)));
        }

        private void OnQuantityChanged(TableConfigurationBuilder<Foo> builder, int value)
        {
            builder.SetValue(x => x.Quantity += value);
        }
    }
}