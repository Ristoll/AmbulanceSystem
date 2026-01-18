**Purpose**
-: Concise guidance for AI coding agents working on this Create React App project.

**Project Summary**
-: Frontend-only React app bootstrapped with Create React App (`react-scripts`). No backend code in this repo. UI is in `src/` and components live in `src/Components/`.
-: Language: UI strings are written in Ukrainian; preserve existing wording and character encodings.

**Quick Commands**
-: Start dev server: `npm start` (runs `react-scripts start`, opens on `http://localhost:3000`).
-: Run tests: `npm test` (CRA interactive runner).
-: Build production bundle: `npm run build`.

**Architecture / Big Picture**
-: Single-page React app (functional components + hooks). Routing via `react-router-dom`.
-: State is mostly local component state (e.g., `Dispatcher.jsx` uses `useState` for `brigades` and `calls`). There is no global store (Redux/MobX) in this repo.
-: Styling: plain CSS files colocated with components (e.g., `src/Components/CallForm.css`). Follow existing CSS-module-like structure: one `.css` per component.

**Common Patterns & Conventions (project-specific)**
-: Components are simple, default-exported functions (e.g., `export default Dispatcher`). Keep this pattern when adding new components.
-: Props and event handlers follow explicit naming like `onSubmit`, `onClose`, `onEdit` (see `CallForm.jsx` and `CallItem.jsx`). When adding handlers, keep names consistent.
-: Localized UI strings are embedded in components (Ukrainian). Do not translate or change language unless requested.
-: Forms use controlled inputs with a single change handler that spreads previous state: `setState(prev => ({...prev, [field]: value}))` (see `CallForm.jsx`). Reuse this pattern for new forms.
-: Temporary UX behavior uses `alert()` and `window.confirm()` for confirmations (e.g., `handleDeleteCall`). When modifying behavior, search for existing `alert`/`confirm` usage to remain consistent.

**Key Files to Edit for Common Tasks**
-: Add new UI route: update `src/index.js` (router) and create component in `src/Components/`.
-: Add new call-related behavior: `src/Components/Dispatcher.jsx`, `src/Components/CallForm.jsx`, `src/Components/CallItem.jsx`.
-: Header / auth buttons: `src/Components/Header.jsx` — these trigger modal open callbacks provided by parent components.

**Examples (copyable snippets & references)**
-: Controlled input pattern (use in new forms):
  ```js
  const handleInputChange = (field, value) => {
    setState(prev => ({ ...prev, [field]: value }));
  };
  ```
-: Creating a new call (see `Dispatcher.jsx`):
  - Build a call object: `{ id: Date.now(), ...callData, createdAt: new Date(), status: 'Новий' }`
  - Prepend to `calls` with `setCalls(prev => [newCall, ...prev])` to keep newest first.

**Testing & Debugging Notes**
-: Use `npm test` for component/unit tests. There are no project-specific test helpers visible.
-: Dev server supports HMR via `npm start`. For runtime inspection, open browser console and React DevTools.

**When to Ask the Human**
-: If a change affects data persistence or requires a backend API, ask for API contracts and mock strategies (there is no API in repo).
-: If you need to change the app language or copy, confirm localization choices before editing Ukrainian strings.

**PR / Commit Guidance for AI Agents**
-: Keep changes minimal and component-scoped. Add/modify a single component per PR where possible.
-: Update or add a `.css` file alongside new components following existing naming (`ComponentName.css`).

If any section here is unclear or you need deeper detail (example data models, desired UX changes, or CI rules), ask and I'll iterate.
