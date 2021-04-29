import './App.css';
import Registro from './components/login/Registro';
import Navbar from './components/Navbar';
import PerfilGeneral from './components/perfiles/PerfilGeneral';
import Table from './components/Table';
import Rutas from './enrutador/Rutas';
import home from './pages/home';
import menu from './pages/menu';


function App() {
  return (
    <div className="App">
      <Rutas />
    </div>
  );
}

export default App;
