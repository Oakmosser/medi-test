/* eslint-disable no-unused-vars */
import logo from '/logo.svg'
import './App.css'
import { useContext, useState } from 'react';
import { Table } from './components/table/table';
import { Button } from './components/button';
import { EndpointContext } from './main';

function App() {

  //Our views are carried in a string state variable which switches depending on the page we're looking at
  const apiEndpoint = useContext(EndpointContext)
  const [currentScans, updateScans] = useState([])
  const [currentNotes, updateNotes] = useState([])
  const [currentView, updateView] = useState("home")

  //Here we get our table fetching methods blocked out. In this case querying the endpoints, taking the JSON object and updating the state tracking the respective data with the newly grabbed values. These can then be used to update and hydrate the components that
  //rely on thjm.
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
          updateNotes({scanid: id, notes: obj});
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
              <Button onClick={() => fetchScanTable()} buttonText={"Scans"} />
            </div>
          </>
        )
      }
      {currentView == "scans" && 
        (
          <Table data={currentScans} updateMethods={{fetchNotesTable, updateView}} currentView={currentView}></Table>
        )
      }
      {currentNotes && currentView == "notes" && 
        (
          <Table data={currentNotes} updateMethods={{updateNotes, updateView}} currentView={currentView}></Table>
        )
      }
      
    </>
  )
}

export default App
