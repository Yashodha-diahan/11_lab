import { useEffect, useState } from 'react'
import './App.css'

// Read the API base URL from the environment (set VITE_API_URL on Vercel/Netlify)
const API = import.meta.env.VITE_API_URL

function App() {
  const [items, setItems] = useState([])
  const [status, setStatus] = useState('loading')
  const [error, setError] = useState('')

  useEffect(() => {
    if (!API) {
      setStatus('error')
      setError('VITE_API_URL is not set — add it in your hosting environment and redeploy.')
      return
    }
    fetch(`${API}/api/menu`)
      .then((res) => {
        if (!res.ok) throw new Error(`API responded with ${res.status}`)
        return res.json()
      })
      .then((data) => {
        setItems(data)
        setStatus('ready')
      })
      .catch((err) => {
        setStatus('error')
        setError(`${err.message}. The API may be waking up (free-tier cold start) — try refreshing in 30–60 s.`)
      })
  }, [])

  return (
    <main className="page">
      <header>
        <h1>CampusEats</h1>
        <p className="tagline">Today's menu at the campus canteen</p>
      </header>

      {status === 'loading' && <p className="info">Loading menu…</p>}
      {status === 'error' && <p className="error">{error}</p>}

      {status === 'ready' && (
        <ul className="menu">
          {items.map((item) => (
            <li key={item.id} className="card">
              <span className="category">{item.category}</span>
              <h2>{item.name}</h2>
              <p className="price">Rs. {Number(item.price).toFixed(2)}</p>
            </li>
          ))}
        </ul>
      )}

      <footer>
        API: <code>{API || 'not configured'}</code> · Mode: <code>{import.meta.env.MODE}</code>
      </footer>
    </main>
  )
}

export default App
