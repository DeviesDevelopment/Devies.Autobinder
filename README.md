# Devies.Autobinder

The project is under development, starting from scratch, should result in an efficient source genarator package, speeding up the creation of CRUD applications.

### Inspiration & Motivation
In usual CRUD APIs with classic repository pattern we have many code repetitions. Guard closes, calling the DB, null checks again. We should be able to remove that, and short circuit the earliest possible.

An initial version of this idea was impleented as an Attribute, but it had to use reflection to work, and it is missing many features, and not really configurable.

Source generators will enable the removal of reflection. That is already a great improvement. Morever rest of the needed features should be straight forward to add. An external library not polluting the codebase is always nice to have.

## Progress

- [x] create repo
- [ ] create implementation plan
- [ ] setup project
- [ ] ...