import './App.css';
//import Registro from './components/login/Registro';
import Navbar from './components/Navbar';
import PerfilGeneral from './components/PerfilGeneral';
import Table from './components/Table';
import home from './pages/home';
import menu from './pages/menu';


function App() {
  return (
    <div className="App">
      <Navbar />
      <PerfilGeneral />
      <Table />
    </div>
  );
}

export default App;
