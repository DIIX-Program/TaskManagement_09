/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./Views/**/*.cshtml",
    "./Views/**/*.razor",
    "./wwwroot/**/*.js"
  ],
  theme: {
    extend: {
      colors: {
        'starbucks-green': '#006241',
        'accent-green': '#00754A',
        'house-green': '#1E3932',
        'uplift-green': '#2b5148',
        'light-green': '#d4e9e2',
        'gold': '#cba258',
        'gold-light': '#dfc49d',
        'warm-neutral': '#f2f0eb',
        'ceramic': '#edebe9',
        'text-black': 'rgba(0, 0, 0, 0.87)',
        'text-black-soft': 'rgba(0, 0, 0, 0.58)',
      },
      fontFamily: {
        'sans': ['Inter', 'SoDoSans', 'Helvetica Neue', 'Helvetica', 'Arial', 'sans-serif'],
        'serif': ['Lander Tall', 'Iowan Old Style', 'Georgia', 'serif'],
        'script': ['Kalam', 'cursive'],
      },
      borderRadius: {
        'pill': '50px',
      },
      boxShadow: {
        'frap': '0 0 6px rgba(0,0,0,0.24), 0 8px 12px rgba(0,0,0,0.14)',
        'soft': '0 2px 4px rgba(0,0,0,0.08)',
      }
    },
  },
  plugins: [],
}
