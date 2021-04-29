module.exports = {
  purge: ['./src/**/*.{js,jsx,ts,tsx}', './public/index.html'],
  darkMode: false, // or 'media' or 'class'
  theme: {
    extend: {
      colors:{
        'azul-pm':'#082d4a',
        'amarillo-pm':'#fabc2e',
        'verde-pm':'#259c98',
        'gris-pm':'#cecfd8',
      }
    },
  },
  variants: {
    extend: {},
  },
  plugins: [],  
}
