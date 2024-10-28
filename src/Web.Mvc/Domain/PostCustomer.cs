using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Web.Mvc.Domain;

public class PostCustomer : Entity, IAggregateRoot
{
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Content { get; set; } = string.Empty;
    [Required]
    public string Link { get; set; } = string.Empty;

    [Required]
    public Guid? ChannelId { get; set; }
    public virtual Channel? Channel { get; set; }

    [Required]
    public Guid? UserId { get; set; }
    public virtual User? User { get; set; }

    [Required]
    public Guid? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }

    [Required]
    public virtual List<PostCard> PostCards { get; set; } = [];

    /// <summary>
    /// Construtor padrão para a classe Produto.
    /// Necessário para a criação de instâncias pelo Entity Framework.
    /// </summary>
    public PostCustomer() { }

    public void addCard(Card card)
    {
        if (card == null) throw new ArgumentNullException(nameof(card), "Card cannot be null.");
    }
}

public class PostCard : Entity
{
    [ForeignKey("PostCustomer")]
    public Guid? PostId { get; set; }
    public virtual PostCustomer? Post { get; set; }
    [Required]
    public Guid? CardId {  get; set; }
    public virtual Card? Card { get; set; }

    /// <summary>
    /// Construtor padrão para a classe Produto.
    /// Necessário para a criação de instâncias pelo Entity Framework.
    /// </summary>
    public PostCard() { }
}

public class Card : Entity, IAggregateRoot
{
    [Required]
    public string Name { get; set; } = string.Empty;   
    [Required]
    public string CardType { get; set; } = string.Empty;
    [Required]
    public int Score { get; set; } = 0;

    /// <summary>
    /// Construtor padrão para a classe Produto.
    /// Necessário para a criação de instâncias pelo Entity Framework.
    /// </summary>
    public Card() { }
}

//public class CardType : Entity, IAggregateRoot
//{
//    public string Name { get; set; }
//}

public class UserCustomer : EntityAudit
{
    [Required]
    public Guid UserId { get; set; } = new Guid();
    public virtual User User { get; set; } = new();

    [Required]
    public Guid CustomerId { get; set; } = new Guid();
    public virtual Customer Custumer { get; set; } = new();

    /// <summary>
    /// Construtor padrão para a classe Produto.
    /// Necessário para a criação de instâncias pelo Entity Framework.
    /// </summary>
    public UserCustomer() { }

}



public class ChannelCustomer : EntityAudit
{
    [Required]
    public Guid CustomerId { get; set; } = new Guid();
    public virtual Customer CanalCliente { get; set; } = new();

    public virtual ICollection<Channel> Channels { get; set; } = [];

}


public class Profile : EntityAudit
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public Gender Gender { get; set; } = new();
    public string Bio { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;

    public Guid UserId { get; set; } = new();
    public virtual User User { get; set; } = new();
}

public class Gender : EntityAudit, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public Gender() { }
}

public class Customer : EntityAudit, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}

public class Channel : EntityAudit, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;

    //public Guid ChannelTypeId { get; set; } = new();
    //public virtual ChannelType ChannelType { get; set; } = new();

    public Channel() { }
}

public class RecordType : EntityAudit, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public RecordType() { }
}

//public class ChannelType : EntityAudit, IAggregateRoot
//{
//    public string Name { get; set; } = string.Empty;
//    public ChannelType() { }
//}

public class PostType : EntityAudit, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public PostType() { }
}
