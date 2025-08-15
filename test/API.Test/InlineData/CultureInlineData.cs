using System.Collections;

namespace API.Test.InlineData;

internal class CultureInlineData : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
        yield return [null];
        yield return ["en-US"];
        yield return ["pt-BR"];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}