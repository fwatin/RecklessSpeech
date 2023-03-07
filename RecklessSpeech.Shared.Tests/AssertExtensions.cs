using FluentAssertions.Equivalency;

namespace RecklessSpeech.Shared.Tests
{
    public static class AssertExtensions
    {
        public static EquivalencyAssertionOptions<TEntity> IgnoreId<TEntity>(
            EquivalencyAssertionOptions<TEntity> option)
            => option.Excluding(ctx => ctx.Name == "Id");

        public static EquivalencyAssertionOptions<TEntity> IgnoreIdAndHtmlContent<TEntity>(
            EquivalencyAssertionOptions<TEntity> option)
            => option.Excluding(ctx => ctx.Name == "HtmlContent" || ctx.Name == "Id");
    }
}