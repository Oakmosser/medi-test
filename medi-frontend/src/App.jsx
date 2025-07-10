/* eslint-disable no-unused-vars */
import logo from '/logo.svg'
import './App.css'
import { useState } from 'react';
import { Table } from './components/table/table';

function App() {

  const apiEndpoint = 'http://localhost:5148';

  const [currentScans, updateScans] = useState([])
  const [currentNotes, updateNotes] = useState([])
  const [currentView, updateView] = useState("home")

  const fetchScanTable = () => 
  {
    const data = fetch(`${apiEndpoint}/scans`, 
    {
      mode: "cors"
    })
    .then(data => data.json())
    .then(obj => 
      {
        updateScans(obj);
        updateView("scans");    
      })
  }

  const fetchNotesTable = (id) => 
  {
    const data = fetch(`${apiEndpoint}/scans/${id}/notes`, 
      {
        mode: "cors"
      })
      .then(data => data.json())
      .then(obj => 
        {
          updateNotes(obj);
          updateView("notes");    
        })
  }

  return (
    <>
      {currentView == "home" && 
        (
          <>
            <div className="flex items-center">
              <a className='m-auto' href="https://singular.health/" target="_blank">
                <img src={logo} className="logo" alt="Singular logo" />
              </a>
            </div>
            <h1>Singular Health Client Scan Portal</h1>
            <div className="card">
              <button onClick={() => fetchScanTable()}>
                Scans
              </button>
            </div>
          </>
        )
      }
      {currentView == "scans" && 
        (
          <Table data={currentScans} updateMethods={{fetchNotesTable, updateView}} currentView={currentView}></Table>
        )
      }
      {currentView == "notes" && 
        (
          <Table data={currentNotes} updateMethods={{updateNotes, updateView}} currentView={currentView}></Table>
        )
      }
      
    </>
  )
}

export default App
