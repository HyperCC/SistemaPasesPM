import './App.css';
import Navbar from './components/Navbar';
import PerfilGeneral from './components/PerfilGeneral';
import Table from './components/Table';
import home from './pages/home';
import menu from './pages/menu';


function App() {
  return (
    <div className="App">
      <Navbar />
      <div className="py-2">
        <PerfilGeneral />
      </div>
      <div className="py-2">
        <Table />
      </div>
      
    </div>
  );
}

export default App;
