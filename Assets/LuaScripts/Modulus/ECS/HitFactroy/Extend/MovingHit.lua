MovingHit = SimpleClass(BaseHit)

function MovingHit:__init(...)

end 

function MovingHit:onExecute()
	local player = EntityMgr:getEntity(self.roleId)
    if player and player.csEntity then 
       player.csEntity:setMoveArgs(self.cfg.distance,self.cfg.moveSpeed,self.cfg.moveAtt,self.cfg.dirType)
    end
end 