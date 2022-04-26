import "./App.css";
import routes from "./routes/routes";
import AppRoute from "./routes/app-route";
import { Suspense } from "react";
import LoadingPage from "./pages/loaders/loading-page";

function App() {
  return (
    <div className="App">
      <Suspense fallback={<LoadingPage />}>
        <AppRoute routes={routes} />
      </Suspense>
    </div>
  );
}

export default App;
