/* eslint-disable no-unused-vars */
import logo from '/logo.svg'
import './App.css'
import { useState } from 'react';
import { TableRow } from './components/table-row';

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
            <div class="flex items-center">
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
          <>
            <table>
              <thead>
                <tr>
                  <th>Id</th>
                  <th>Date Created</th>
                  <th>Patient</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {
                  currentScans.map(scan => <TableRow scan={scan} updateMethods={{updateNotes}} />)
                }
              </tbody>
            </table>
          </>
        )
      }
      {currentView == "notes" && 
        (
          <>
            <table>
              <thead>
                <th>Id</th>
                <th>Date Created</th>
                <th>Title</th>
                <th>Content</th>
              </thead>
              <tbody>
                {
                  currentNotes.map(note => <TableRow note={note}/>)
                }
              </tbody>
            </table>
          </>
        )
      }
      
    </>
  )
}

export default App
