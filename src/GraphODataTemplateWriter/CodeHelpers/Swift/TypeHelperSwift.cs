// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.Swift
{
    using Vipr.Core.CodeModel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Graph.ODataTemplateWriter.Extensions;
    using Microsoft.Graph.ODataTemplateWriter.Settings;

    public static class TypeHelperSwift
    {
        public static string Prefix = ConfigurationService.Settings.NamespacePrefix;
        public const string DefaultReservedPostfix = "_";
        public static ICollection<string>  GetReservedNames()
        {
            return new HashSet<string>(StringComparer.Ordinal)
            {
                "associatedtype", "class", "deinit", "enum", "extension", "fileprivate", "func", "import", "init", "inout", "internal", "let", "open", "operator", "private", "protocol", "public", "static", "struct", "subscript", "typealias", "var",
                "break", "case", "continue", "default", "defer", "do", "else", "fallthrough", "for", "guard", "if", "in", "repeat", "return", "switch", "where", "while",
                "as", "Any", "catch", "false", "is", "nil", "rethrows", "super", "self", "Self", "throw", "throws", "true", "try",
                "#available", "#colorLiteral", "#column", "#else", "#elseif", "#endif", "#file", "#fileLiteral", "#function", "#if", "#imageLiteral", "#line", "#selector", "#sourceLocation",
                "associativity", "convenience", "dynamic", "didSet", "final", "get", "infix", "indirect", "lazy", "left", "mutating", "none", "nonmutating", "optional", "override", "postfix", "precedence", "prefix", "Protocol", "required", "right", "set", "Type", "unowned", "weak", "willSet",
                "operator"
            };
        }

        private static readonly ICollection<string> SimpleTypes =
            new HashSet<string> (StringComparer.OrdinalIgnoreCase)
            {
                "String",
                "Float",
                "Date",
                "Bool",
                "Stream"
            };

      public static string GetTypeString(this OdcmParameter parameter)
      {
          return GetTypeString(parameter.Type);
      }
      public static string GetTypeString(this OdcmProperty property)
      {
          return GetTypeString(property.Projection.Type);
      }

      public static string GetTypeString(this OdcmType type)
      {
          if (type == null)
          {
              return "Any";
          }
          switch (type.Name) {
              case "String":
              case "Int32":
              case "Int64":
              case "Int16":
              case "Date":
                  return type.Name;
              case "Guid":
                  return "String";
              case "Double":
              case "Float":
                  return "Float";
              case "DateTimeOffset":
                  return "Date";
              case "Binary":
                  return "String";
              case "Boolean":
                  return "Bool";
              case "Stream":
                  return "Stream";
              case "Json":
                  return "Any";
              default:
                  return Prefix + type.Name.ToUpperFirstChar();
          }
      }

      public static string SanitizeName(this string name) {
            if (GetReservedNames().Contains(name))
            {
                return string.Concat(name, DefaultReservedPostfix);
            }
            return name;
      }

        public static bool IsComplex(this OdcmProperty property)
        {
            return property.Projection.Type.IsComplex();
        }

        public static bool IsComplex(this OdcmParameter property)
        {
            string t = property.GetTypeString();
            return t.IsComplex();
        }

        public static bool IsComplex(this OdcmType type)
        {
            string t = type.GetTypeString();
            return t.IsComplex();
        }

        public static bool IsComplex(this string t)
        {
            return !TypeHelperSwift.SimpleTypes.Contains(t);
        }

    }
}