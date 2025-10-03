using System;
using System.Collections.Generic;
using System.Linq;

namespace GanLink.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

using Humanizer;
  
public static class StringExtension  
{  
    public static string ToSnakeCase(this string input)  
    {        return new string(Convert(input.GetEnumerator()).ToArray());  
  
        static IEnumerable<char> Convert(CharEnumerator e)  
        {            if(!e.MoveNext()) yield break;  
            yield return char.ToLower(e.Current);  
  
            while (e.MoveNext())  
            {                if (char.IsUpper(e.Current))  
            {                    yield return '_';  
                yield return char.ToLower(e.Current);  
            }                else  
            {  
                yield return e.Current;  
            }            }        }    }  
    public static string ToPlural(this string text)  
    {        return text.Pluralize(false);  
    }}