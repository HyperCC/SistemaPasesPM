import './App.css';
import React, { useState, useEffect } from "react";
import Registro from './components/login/Registro';
import Navbar from './components/Navbar';
import PerfilGeneral from './components/perfiles/PerfilGeneral';
import Table from './components/Table';
import Rutas from './enrutador/Rutas';
import home from './pages/home';
import menu from './pages/menu';
import { useStateValue } from "./contexto/Store";
import { perfilUsuario } from './actions/UsuarioAction';


function App() {

  const [dispatch] = useStateValue();

  const [iniciaApp, setIniciaApp] = useState(false);

  useEffect(() => {
    if (!iniciaApp) {
      perfilUsuario(dispatch)
        .then((response) => {
          setIniciaApp(true);
        })
        .catch((error) => {
          setIniciaApp(true);
        });
    }
  }, [iniciaApp]);

  return iniciaApp === false ? null : (
    <div className="App">
      <Rutas />
    </div>
  );
}

export default App;
