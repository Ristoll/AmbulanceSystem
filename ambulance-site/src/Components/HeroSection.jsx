import './HeroSection.css';

function HeroSection() {
  return (
    <section className="hero"
      style={{ backgroundImage: 'url(/images/ambulance-bg.jpg)' }}>
      <div className="hero-content">
        <h1>Ми поруч, коли потрібна допомога</h1>
        <p>Оперативна медична допомога 24/7 по всій Україні</p>
        <button className="call-button">📞 Викликати швидку онлайн</button>
      </div>
    </section>
  );
}

export default HeroSection;
