import './App.css';
import React, { useState, useEffect } from "react";
import Rutas from './enrutador/Rutas';
import { useStateValue } from "./contexto/store";
import { perfilUsuario } from './actions/UsuarioAction';


function App() {

  const [{ usuarioSesion }, dispatch] = useStateValue();

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

  return iniciaApp == false ? null : (
    <div className="App">
      <Rutas />
    </div>
  );
}

export default App;
