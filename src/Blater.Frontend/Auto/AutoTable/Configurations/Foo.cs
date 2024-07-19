using Blater.Frontend.Auto.AutoTable.Implementations;
using Blater.Frontend.Auto.AutoTable.Interfaces;
using Blater.Models.Bases;

namespace Blater.Frontend.Auto.AutoTable.Configurations;

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
                .HasColumnName("Qtd")
                .MaxLength(5)
                .MergeColumn(2)
                .Class("mud-width-full")
                .Style("color: red")
                .IsRequired();
        }
    }
}