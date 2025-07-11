import { createContext, StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'

/* Got this context here to persist endpoint host throughout front end */
export const EndpointContext = createContext('http://localhost:5148');

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <EndpointContext value='http://localhost:5148'>
      <App />
    </EndpointContext>
  </StrictMode>
)
