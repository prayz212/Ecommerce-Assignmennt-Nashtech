import Loader from "../../components/loader/loader";

const LoadingPage = () => {
  return (
    <div className="flex h-screen fixed top-0 right-0 left-0 bg-slate-100 opacity-30">
      <div className="m-auto">
        <Loader />
      </div>
    </div>
  );
};

export default LoadingPage;