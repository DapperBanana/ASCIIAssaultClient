# ASCII Assault Client - Development Plan

## Completed
- [x] Project scaffolding and initial repo setup
- [x] TCP client connection handling with threaded message listener
- [x] Basic MainForm with console-style output, message input, connect button
- [x] AsciiRenderer utility class for brogue-style glyph rendering

## In Progress
- [ ] Integrate AsciiRenderer into a dedicated game panel on MainForm
- [ ] Wire up server map data to renderer (parse incoming map strings)

## Upcoming
- [ ] Pretty up the client form (dark theme polish, layout refinement)
- [ ] Test rendering with sample dungeon maps
- [ ] Add color variation / flickering for brogue-style atmosphere
- [ ] Handle disconnect/reconnect gracefully
- [ ] Look into double-buffered panel for smooth ASCII rendering
