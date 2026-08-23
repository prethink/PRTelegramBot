---
description: Generator for various kinds of data.
---

# Generator

Generator for various kinds of data.

## Values

| Value | Description |
| --- | --- |
| `Digits` | Digits 0-9. |
| `Alphabet` | Latin letters. |
| `Symbols` | Punctuation and special characters. |

## Methods

| Method | Description |
| --- | --- |
| `static string RandomSymbols(Chars chars, int length, string prefix = "")` | Generates a random character set. |
| `static string Coupon(int segmentLength = 6, int countSplit = 1, char symbolSplit = '-')` | Generates a coupon. Can be used for various campaigns or promo codes. |

