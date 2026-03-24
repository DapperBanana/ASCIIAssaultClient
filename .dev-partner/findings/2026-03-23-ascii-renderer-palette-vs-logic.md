---
title: AsciiRenderer tile mapping — keep palette separate from render logic
date: 2026-03-23
tags: [rendering, pattern, c#]
type: pattern
project: DapperBanana/ASCIIAssaultClient
---

If AsciiRenderer is handling both tile-to-character mapping and color assignment, you're mixing data (palette) with behavior. Extract tile definitions into a constant dictionary or config — maybe something like `Dictionary<TileType, (char glyph, ConsoleColor color)>`.

This scales better for brogue-style games where you'll want swappable themes (winter palette, retro monochrome, etc.). It also makes testing the renderer itself trivial — no dependency on game state.

If you're doing any rotation or scaling transforms on ASCII art (tilting the view, zooming), keep that math in the renderer but feed it palette-agnostic tile IDs from the model layer.
