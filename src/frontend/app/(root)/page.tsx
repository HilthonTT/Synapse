import { getUserFromAuth } from "@/actions/user";

const HomePage = async () => {
  const currentUser = await getUserFromAuth();

  return <div>Home</div>;
};

export default HomePage;
