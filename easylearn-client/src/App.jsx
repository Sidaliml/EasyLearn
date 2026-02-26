import { useEffect, useState } from "react";

const API_BASE = "https://localhost:7232/api";

export default function App() {
  const [subjects, setSubjects] = useState([]);
  const [assignments, setAssignments] = useState([]);

  const [selectedSubject, setSelectedSubject] = useState(null);

  // Form state
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [deadline, setDeadline] = useState(""); // yyyy-mm-dd

  // Hämta ämnen + uppgifter vid start
  useEffect(() => {
    fetch(`${API_BASE}/Subjects`)
      .then((res) => res.json())
      .then((data) => setSubjects(data))
      .catch((err) => console.error("Fel vid Subjects:", err));

    fetch(`${API_BASE}/Assignments`)
      .then((res) => res.json())
      .then((data) => setAssignments(data))
      .catch((err) => console.error("Fel vid Assignments:", err));
  }, []);

  // Skapa uppgift (POST)
  async function handleCreateAssignment(e) {
    e.preventDefault();

    if (!selectedSubject) {
      alert("Välj ett ämne först!");
      return;
    }

    if (!title.trim()) {
      alert("Skriv en titel!");
      return;
    }

    // deadline måste vara en korrekt ISO-datumsträng för backend
    // Vi gör: "YYYY-MM-DDT00:00:00"
    const payload = {
      subjectId: selectedSubject.id,
      title: title.trim(),
      description: description.trim(),
      deadline: deadline ? `${deadline}T00:00:00` : null,
    };

    try {
      const res = await fetch(`${API_BASE}/Assignments`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload),
      });

      if (!res.ok) {
        const text = await res.text();
        throw new Error(text || "Kunde inte skapa uppgift");
      }

      const created = await res.json();

      // Lägg till direkt i listan så du ser den utan refresh
      setAssignments((prev) => [created, ...prev]);

      // Rensa formulär
      setTitle("");
      setDescription("");
      setDeadline("");

      alert("✅ Uppgift skapad!");
    } catch (err) {
      console.error(err);
      alert("❌ Fel: " + err.message);
    }
  }

  // Filtrera uppgifter efter valt ämne
  const filteredAssignments = selectedSubject
    ? assignments.filter((a) => a.subjectId === selectedSubject.id)
    : [];

  return (
    <div style={{ padding: "2rem", fontFamily: "Arial" }}>
      <h1>EasyLearn</h1>

      <h2>Ämnen</h2>
      {subjects.length === 0 ? (
        <p>Inga ämnen hittades.</p>
      ) : (
        <ul>
          {subjects.map((s) => (
            <li key={s.id} style={{ marginBottom: "0.5rem" }}>
              <strong>{s.name}</strong>{" "}
              <button onClick={() => setSelectedSubject(s)}>
                Visa uppgifter
              </button>
            </li>
          ))}
        </ul>
      )}

      <hr style={{ margin: "2rem 0" }} />

      <h2>Uppgifter</h2>

      {!selectedSubject ? (
        <p>Välj ett ämne för att se uppgifter.</p>
      ) : (
        <>
          <p>
            <strong>Ämne:</strong> {selectedSubject.name}
          </p>

          {/* FORM: skapa uppgift */}
          <h3>Skapa ny uppgift</h3>
          <form onSubmit={handleCreateAssignment} style={{ maxWidth: "400px" }}>
            <div style={{ marginBottom: "0.75rem" }}>
              <label>Titel</label>
              <input
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                style={{ width: "100%", padding: "0.4rem" }}
              />
            </div>

            <div style={{ marginBottom: "0.75rem" }}>
              <label>Beskrivning</label>
              <textarea
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                style={{ width: "100%", padding: "0.4rem" }}
                rows={3}
              />
            </div>

            <div style={{ marginBottom: "0.75rem" }}>
              <label>Deadline</label>
              <input
                type="date"
                value={deadline}
                onChange={(e) => setDeadline(e.target.value)}
                style={{ width: "100%", padding: "0.4rem" }}
              />
            </div>

            <button type="submit">Skapa uppgift</button>
          </form>

          <hr style={{ margin: "2rem 0" }} />

          {/* LISTA */}
          {filteredAssignments.length === 0 ? (
            <p>Inga uppgifter för detta ämne.</p>
          ) : (
            <ul>
              {filteredAssignments.map((a) => (
                <li key={a.id} style={{ marginBottom: "1rem" }}>
                  <strong>{a.title}</strong>
                  <div>{a.description}</div>
                  <div>Deadline: {a.deadline}</div>
                  <div>
                    Status: {a.isCompleted ? "✅ Klar" : "❌ Inte klar"}
                  </div>
                </li>
              ))}
            </ul>
          )}
        </>
      )}
    </div>
  );
}